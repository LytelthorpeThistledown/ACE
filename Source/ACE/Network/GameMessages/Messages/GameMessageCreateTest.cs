using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Network.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace ACE.Network.GameMessages.Messages
{
    public class GameMessageCreateTest : GameMessage
    {
        private static uint nextObjectId = 100;
        protected static Dictionary<string, string> dataDict = new Dictionary<string, string>();

        public GameMessageCreateTest(Player player, string param) : base(GameMessageOpcode.ObjectCreate, GameMessageGroup.Group0A)
        {
            this.LoadData();

            var data = new string[0];

            int paramIndex;
            if (int.TryParse(param, out paramIndex))
                data = dataDict[dataDict.Keys.ToList()[paramIndex]].Split('|');
            else
                data = dataDict[param].Split('|');

            var dataIndex = 0;
            var wcid = ushort.Parse(data[dataIndex++]);
            var name = data[dataIndex++];
            var pluralname = data[dataIndex++];
            var type = uint.Parse(data[dataIndex++]);
            var iconid = uint.Parse(data[dataIndex++]);
            var iconoverlayid = uint.Parse(data[dataIndex++]);
            var iconunderlayid = uint.Parse(data[dataIndex++]);
            var setupid = uint.Parse(data[dataIndex++]);
            var animframeid = uint.Parse(data[dataIndex++]);
            var phstableid = uint.Parse(data[dataIndex++]);
            var stableid = uint.Parse(data[dataIndex++]);
            var itemscapacity = byte.Parse(data[dataIndex++]);
            var effects = uint.Parse(data[dataIndex++]);
            var ammotype = uint.Parse(data[dataIndex++]);
            var combatuse = uint.Parse(data[dataIndex++]);
            var maxstructure = uint.Parse(data[dataIndex++]);
            var maxstacksize = uint.Parse(data[dataIndex++]);
            var validlocations = uint.Parse(data[dataIndex++]);
            var targettype = uint.Parse(data[dataIndex++]);
            var burden = uint.Parse(data[dataIndex++]);
            var spellid = uint.Parse(data[dataIndex++]);
            var hooktype = uint.Parse(data[dataIndex++]);
            var materialtype = uint.Parse(data[dataIndex++]);
            var cooldownid = uint.Parse(data[dataIndex++]);
            var cooldownduration = uint.Parse(data[dataIndex++]);
            var objectdescription = uint.Parse(data[dataIndex++]);
            var physicsstate = uint.Parse(data[dataIndex++]);
            var containerscapacity = uint.Parse(data[dataIndex++]);
            var paletteid = uint.Parse(data[dataIndex++]);
            var palettedata = data[dataIndex++];
            var texturedata = data[dataIndex++];
            var modeldata = data[dataIndex++];

            uint value = 0;
            uint useable = 1;
            uint radius = 0;
            uint monarch = 0;
            uint structure = 0;
            uint stacksize = 0;
            uint wielder = 0;
            uint location = 0;
            uint priority = 0;
            uint blipcolor = 0;
            uint radar = 0;
            uint workmanship = 0;
            uint houseowner = 0;
            uint houserestrictions = 0;
            uint script = 0;
            uint hookitemtypes = 0;

            var position = this.GetPosition(player);

            var weenie = this.GetWeenie(pluralname, itemscapacity, value, useable, radius, monarch, effects, ammotype, combatuse,
                structure, maxstructure, stacksize, maxstacksize, containerscapacity, wielder, validlocations, location, priority,
                targettype, blipcolor, burden, spellid, radar, workmanship, houseowner, houserestrictions, script, hooktype,
                hookitemtypes, iconoverlayid, materialtype);

            var bWO = new ImmutableWorldObject((ObjectType)type, new ObjectGuid(nextObjectId++), name, wcid, (ObjectDescriptionFlag)objectdescription, weenie, position);

            var bPhysicsDesc = PhysicsDescriptionFlag.Position | PhysicsDescriptionFlag.Stable | PhysicsDescriptionFlag.Petable | PhysicsDescriptionFlag.CSetup;
            bWO.PhysicsData.PhysicsDescriptionFlag = bPhysicsDesc;
            if ((physicsstate & (uint)PhysicsState.Hidden) != 0)
                bWO.PhysicsData.PhysicsState = (PhysicsState)physicsstate ^ PhysicsState.Hidden; //  remove hidden so we can see it

            bWO.PhysicsData.Stable = stableid;
            bWO.PhysicsData.Petable = phstableid;
            bWO.PhysicsData.CSetup = setupid;

            // these were defaulting to 1, set to 0
            bWO.PhysicsData.PositionSequence = 0;
            bWO.PhysicsData.unknownseq0 = 0;
            bWO.PhysicsData.PhysicsSequence = 0;
            bWO.PhysicsData.JumpSequence = 0;
            bWO.PhysicsData.PortalSequence = 0;
            bWO.PhysicsData.unknownseq1 = 0;
            bWO.PhysicsData.SpawnSequence = 0;

            bWO.Icon = iconid;
            bWO.GameData.ItemCapacity = itemscapacity;
            bWO.GameData.ContainerCapacity = (byte)containerscapacity;
            bWO.GameData.RadarBehavior = RadarBehavior.ShowAlways;


            bWO.ModelData.PalleteGuid = paletteid;

            if (!string.IsNullOrEmpty(palettedata))
                foreach (var palette in palettedata.Split('^'))
                {
                    var parts = palette.Split(',');
                    uint id = uint.Parse(parts[0]);
                    byte offset = (byte)(int.Parse(parts[1]) / 8);
                    byte length = 0;
                    int lengthTemp = int.Parse(parts[2]);
                    if (lengthTemp == 256)
                        length = 0;
                    else
                        length = (byte)((int)lengthTemp / 8);

                    bWO.ModelData.AddPallet(id, offset, length);
                }

            if (!string.IsNullOrEmpty(texturedata))
                foreach (var texture in texturedata.Split('^'))
                {
                    var parts = texture.Split(',');
                    byte index = byte.Parse(parts[0]);
                    uint oldid = uint.Parse(parts[1]);
                    uint newid = uint.Parse(parts[2]);

                    bWO.ModelData.AddTexture(index, oldid, newid);
                }

            if (!string.IsNullOrEmpty(modeldata))
                foreach (var model in modeldata.Split('^'))
                {
                    var parts = model.Split(',');
                    byte index = byte.Parse(parts[0]);
                    uint resourceId = uint.Parse(parts[1]);

                    bWO.ModelData.AddModel(index, resourceId);
                }

            bWO.SerializeCreateObject(Writer);
        }

        private Position GetPosition(Player player)
        {
            float qw = player.Position.Facing.W; // north
            float qz = player.Position.Facing.Z; // south

            double x = 2 * qw * qz;
            double y = 1 - 2 * qz * qz;

            var heading = Math.Atan2(x, y);
            double scalar = 3.0f;
            var dx = -1 * Convert.ToSingle(Math.Sin(heading) * scalar);
            var dy = Convert.ToSingle(Math.Cos(heading) * scalar);

            Position newPosition = new Position(player.Position.LandblockId.Raw, player.Position.Offset.X + dx, player.Position.Offset.Y + dy, player.Position.Offset.Z + 0.5f, 0f, 0f, 0f, 0f);

            return newPosition;
        }

        private WeenieHeaderFlag GetWeenie(string pluralname, byte itemscapacity, uint value, uint useable, uint radius, uint monarch, uint effect
            , uint ammotype, uint combatuse, uint structure, uint maxstructure, uint stacksize, uint maxstacksize, uint containerscapacity, uint wielder
            , uint validlocations, uint location, uint priority, uint targettype, uint blipcolor, uint burden, uint spellid, uint radar, uint workmanship
            , uint houseowner, uint houserestrictions, uint script, uint hooktype, uint hookitemtypes, uint iconoverlay, uint material)
        {
            var weenie = WeenieHeaderFlag.None;

            //    PuralName = 0x00000001,
            if (!string.IsNullOrEmpty(pluralname))
                weenie |= WeenieHeaderFlag.PuralName;

            //ItemCapacity = 0x00000002,
            if (itemscapacity > 0)
                weenie |= WeenieHeaderFlag.ItemCapacity;

            //ContainerCapacity = 0x00000004,
            if (containerscapacity > 0)
                weenie |= WeenieHeaderFlag.ContainerCapacity;

            //Value = 0x00000008,
            if (value > 0)
                weenie |= WeenieHeaderFlag.Value;

            //Usable = 0x00000010,
            if (useable > 0)
                weenie |= WeenieHeaderFlag.Usable;

            //UseRadius = 0x00000020,
            if (radius > 0)
                weenie |= WeenieHeaderFlag.UseRadius;

            //Monarch = 0x00000040,
            if (monarch > 0)
                weenie |= WeenieHeaderFlag.Monarch;

            //UiEffects = 0x00000080,
            if (effect > 0)
                weenie |= WeenieHeaderFlag.UiEffects;

            //AmmoType = 0x00000100,
            if (ammotype > 0)
                weenie |= WeenieHeaderFlag.AmmoType;

            //CombatUse = 0x00000200,
            if (combatuse > 0)
                weenie |= WeenieHeaderFlag.CombatUse;

            //Struture = 0x00000400,
            if (structure > 0)
                weenie |= WeenieHeaderFlag.Struture;

            //MaxStructure = 0x00000800,
            if (maxstructure > 0)
                weenie |= WeenieHeaderFlag.MaxStructure;

            //StackSize = 0x00001000,
            if (stacksize > 0)
                weenie |= WeenieHeaderFlag.StackSize;

            //MaxStackSize = 0x00002000,
            if (maxstacksize > 0)
                weenie |= WeenieHeaderFlag.MaxStackSize;

            //Container = 0x00004000,
            if (containerscapacity > 0)
                weenie |= WeenieHeaderFlag.Container;

            //Wielder = 0x00008000,
            if (wielder > 0)
                weenie |= WeenieHeaderFlag.Wielder;

            //ValidLocations = 0x00010000,
            if (validlocations > 0)
                weenie |= WeenieHeaderFlag.ValidLocations;

            //Location = 0x00020000,
            if (location > 0)
                weenie |= WeenieHeaderFlag.Location;

            //Priority = 0x00040000,
            if (priority > 0)
                weenie |= WeenieHeaderFlag.Priority;

            //TargetType = 0x00080000,
            if (targettype > 0)
                weenie |= WeenieHeaderFlag.TargetType;

            //BlipColour = 0x00100000,
            if (blipcolor > 0)
                weenie |= WeenieHeaderFlag.BlipColour;

            //Burden = 0x00200000,
            if (burden > 0)
                weenie |= WeenieHeaderFlag.Burden;

            //Spell = 0x00400000,
            if (spellid > 0)
                weenie |= WeenieHeaderFlag.Spell;

            //Radar = 0x00800000,
            if (radar > 0)
                weenie |= WeenieHeaderFlag.Radar;

            //Workmanship = 0x01000000,
            if (workmanship > 0)
                weenie |= WeenieHeaderFlag.Workmanship;

            //HouseOwner = 0x02000000,
            if (houseowner > 0)
                weenie |= WeenieHeaderFlag.HouseOwner;

            //HouseRestrictions = 0x04000000,
            if (houserestrictions > 0)
                weenie |= WeenieHeaderFlag.HouseRestrictions;

            //Script = 0x08000000,
            if (script > 0)
                weenie |= WeenieHeaderFlag.Script;

            //HookType = 0x10000000,
            if (hooktype > 0)
                weenie |= WeenieHeaderFlag.HookType;

            //HookItemTypes = 0x20000000,
            if (hookitemtypes > 0)
                weenie |= WeenieHeaderFlag.HookItemTypes;

            //IconOverlay = 0x40000000,
            if (iconoverlay > 0)
                weenie |= WeenieHeaderFlag.IconOverlay;

            //Material = 0x80000000
            if (material > 0)
                weenie |= WeenieHeaderFlag.Material;

            return weenie;
        }

        private void LoadData()
        {
            if (dataDict.Count > 0)
                return;

            var strings = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ObjectData.txt"));

            foreach (var str in strings)
            {
                var parts = str.Split('|');
                dataDict.Add(parts[1], str);
            }
        }
    }
}