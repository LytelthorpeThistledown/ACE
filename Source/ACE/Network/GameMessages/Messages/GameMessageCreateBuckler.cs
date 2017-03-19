using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Network.Enum;
using System;
using System.Linq;
using System.Numerics;

namespace ACE.Network.GameMessages.Messages
{
    public class GameMessageCreateBuckler : GameMessage
    {
        /// <summary>
        /// test creating stuff
        /// </summary>
        public GameMessageCreateBuckler(Player player) : base(GameMessageOpcode.ObjectCreate, GameMessageGroup.Group0A)
        {
            /*
            // buckler in pack
            var bWeenie = WeenieHeaderFlag.Value | WeenieHeaderFlag.Usable | WeenieHeaderFlag.CombatUse | WeenieHeaderFlag.Container | WeenieHeaderFlag.ValidLocations | WeenieHeaderFlag.Burden | WeenieHeaderFlag.HookType;
            var bWO = new ImmutableWorldObject(ObjectType.Armor, new ObjectGuid(2872552533), "Buckler", ObjectDescriptionFlag.Inscribable | ObjectDescriptionFlag.Attackable, bWeenie, new Position(0, 0f, 0f, 0f, 0f, 0f, 0f, 0f));

            bWO.ModelData.PalleteGuid = 3055;
            bWO.ModelData.AddPallet(3058, 0, 0);
            bWO.ModelData.AddTexture(0, 4057, 4056);
            bWO.ModelData.AddModel(0, 1104);

            var bPhysicsDesc = PhysicsDescriptionFlag.Position | PhysicsDescriptionFlag.Stable | PhysicsDescriptionFlag.Petable | PhysicsDescriptionFlag.CSetup | PhysicsDescriptionFlag.Parent | PhysicsDescriptionFlag.ObjScale;
            bWO.PhysicsData.PhysicsDescriptionFlag = bPhysicsDesc;
            var bPhysicsState = (PhysicsState)1044; // TODO: fix this
            bWO.PhysicsData.PhysicsState = bPhysicsState;
            bWO.PhysicsData.Stable = 536870932;
            bWO.PhysicsData.Petable = 872415275;
            bWO.PhysicsData.CSetup = 33554786;
            //bWO.PhysicsData.Parent = player.Guid.Full;
            //bWO.PhysicsData.Location = 3;
            bWO.PhysicsData.ObjScale = .5f;
            bWO.PhysicsData.PortalSequence = 2;
            bWO.PhysicsData.SpawnSequence = 56705;

            bWO.GameDataType = 44;
            bWO.Icon = 5158;
            bWO.GameData.Value = 1100;
            bWO.GameData.Usable = Usable.UsableNo;
            bWO.GameData.CombatUse = CombatUse.Shield;
            bWO.GameData.Wielder = player.Guid.Full;
            bWO.GameData.ValidLocations = EquipMask.Shield;
            //bWO.GameData.Location = EquipMask.Shield;
            bWO.GameData.Burden = 420;
            bWO.GameData.HookType = 2;
            bWO.GameData.ContainerId = player.Guid.Full;

            bWO.SerializeCreateObject(Writer);
            */

            /*
            // buckler in front
            float qw = player.Position.Facing.W; // north
            float qz = player.Position.Facing.Z; // south

            double x = 2 * qw * qz;
            double y = 1 - 2 * qz * qz;

            var heading = Math.Atan2(x, y);
            double scalar = 3.0f;
            var dx = -1 * Convert.ToSingle(Math.Sin(heading) * scalar);
            var dy = Convert.ToSingle(Math.Cos(heading) * scalar);

            Position newPosition = new Position(player.Position.Cell, player.Position.Offset.X + dx, player.Position.Offset.Y + dy, player.Position.Offset.Z + 0.5f, 0f, 0f, 0f, 0f);


            // buckler
            var bWeenie = WeenieHeaderFlag.Value | WeenieHeaderFlag.Usable | WeenieHeaderFlag.CombatUse | WeenieHeaderFlag.ValidLocations | WeenieHeaderFlag.Burden | WeenieHeaderFlag.HookType;
            var bWO = new ImmutableWorldObject(ObjectType.Armor, new ObjectGuid(2872552533), "Buckler", ObjectDescriptionFlag.Inscribable | ObjectDescriptionFlag.Attackable, bWeenie, newPosition);

            bWO.ModelData.PalleteGuid = 3055;
            bWO.ModelData.AddPallet(3058, 0, 0);
            bWO.ModelData.AddTexture(0, 4057, 4056);
            bWO.ModelData.AddModel(0, 1104);

            var bPhysicsDesc = PhysicsDescriptionFlag.Position | PhysicsDescriptionFlag.Stable | PhysicsDescriptionFlag.Petable | PhysicsDescriptionFlag.CSetup | PhysicsDescriptionFlag.Parent | PhysicsDescriptionFlag.ObjScale;
            bWO.PhysicsData.PhysicsDescriptionFlag = bPhysicsDesc;
            var bPhysicsState = (PhysicsState)1044; // TODO: fix this
            bWO.PhysicsData.PhysicsState = bPhysicsState;
            bWO.PhysicsData.Stable = 536870932;
            bWO.PhysicsData.Petable = 872415275;
            bWO.PhysicsData.CSetup = 33554786;
            //bWO.PhysicsData.Parent = player.Guid.Full;
           // bWO.PhysicsData.Location = 3;
            bWO.PhysicsData.ObjScale = .5f;
            bWO.PhysicsData.PortalSequence = 2;
            bWO.PhysicsData.SpawnSequence = 56705;

            bWO.GameDataType = 44;
            bWO.Icon = 5158;
            bWO.GameData.Value = 1100;
            bWO.GameData.Usable = Usable.UsableNo;
            bWO.GameData.CombatUse = CombatUse.Shield;
            //bWO.GameData.Wielder = player.Guid.Full;
            bWO.GameData.ValidLocations = EquipMask.Shield;
            //bWO.GameData.Location = EquipMask.Shield;
            bWO.GameData.Burden = 420;
            bWO.GameData.HookType = 2;

            bWO.SerializeCreateObject(Writer);
            */

            // buckler
            var bWeenie = WeenieHeaderFlag.Value | WeenieHeaderFlag.Usable | WeenieHeaderFlag.CombatUse | WeenieHeaderFlag.Wielder | WeenieHeaderFlag.ValidLocations | WeenieHeaderFlag.Location | WeenieHeaderFlag.Burden | WeenieHeaderFlag.HookType;
            var bWO = new ImmutableWorldObject(ObjectType.Armor, new ObjectGuid(2872552533), "Buckler", 44, ObjectDescriptionFlag.Inscribable | ObjectDescriptionFlag.Attackable, bWeenie, new Position(0, 0f, 0f, 0f, 0f, 0f, 0f, 0f));

            bWO.ModelData.PalleteGuid = 3055;
            bWO.ModelData.AddPallet(3058, 0, 0);
            bWO.ModelData.AddTexture(0, 4057, 4056);
            bWO.ModelData.AddModel(0, 1104);

            var bPhysicsDesc = PhysicsDescriptionFlag.Position | PhysicsDescriptionFlag.Stable | PhysicsDescriptionFlag.Petable | PhysicsDescriptionFlag.CSetup | PhysicsDescriptionFlag.Parent | PhysicsDescriptionFlag.ObjScale;
            bWO.PhysicsData.PhysicsDescriptionFlag = bPhysicsDesc;
            var bPhysicsState = (PhysicsState)1044; // TODO: fix this
            bWO.PhysicsData.PhysicsState = bPhysicsState;
            bWO.PhysicsData.Stable = 536870932;
            bWO.PhysicsData.Petable = 872415275;
            bWO.PhysicsData.CSetup = 33554786;
            bWO.PhysicsData.Parent = player.Guid.Full;
            bWO.PhysicsData.Location = 3;
            bWO.PhysicsData.ObjScale = 1.5f;
            bWO.PhysicsData.PortalSequence = 2;
            bWO.PhysicsData.SpawnSequence = 56705;

            bWO.Icon = 100668454u;
            bWO.GameData.Value = 1100;
            bWO.GameData.Usable = Usable.UsableNo;
            bWO.GameData.CombatUse = CombatUse.Shield;
            bWO.GameData.Wielder = player.Guid.Full;
            bWO.GameData.ValidLocations = EquipMask.Shield;
            bWO.GameData.Location = EquipMask.Shield;
            bWO.GameData.Burden = 420;
            bWO.GameData.HookType = 2;

            bWO.SerializeCreateObject(Writer);

            //Contract for Kill: Rynthid Rifts
            //Writer.Write(StringToByteArray("59A441DC11000000011802001404000065000000140000202B000034550100020200000000000000000000000000000002000000987008002000436F6E747261637420666F72204B696C6C3A2052796E7468696420526966747300000080A3CA0080D86F000800001200000406000000640000000800000010000000020000000100010004000050640000000000000000000040"));
            // buckler
            //Writer.Write(StringToByteArray("55AC37AB11010101EF0BF20B000000D90FD80F0050040000A1980000140400000200A64BEEDC22412B2BFF41A418A842F6D1093F6D6D1B3EA72D54BF47AA5E3C140000202B0000346201000204000050030000000000013F0200000000000000000000000000000081DD00001882231007004275636B6C65720000002C00261402000000120000004C0400000100000004040000500000200000002000A4010200000000"));
            // acid tachi
            //Writer.Write(StringToByteArray("24AC37AB11010301EF0BF00B0000009D029D02009B029B02009A029A0200BB022198020014040000010000000200A64BC3062341EC1900422E1AA8423BAB0A3F3BAB0A3FC6B6E8BEC6B6E8BE140000202B0000341205000204000050010000000200000000000000000000000000000081DD0000988223100A0041636964205461636869008005BAF41501000000120000000000CC010000010000000001000001040000500000100000001000C2010200000000"));

            //Writer.Write(StringToByteArray("00144F82110404017E00D40E2828B005500CB005740C8A04600C00D5037C1800D4037D1800B00B791800BE0C7818007F04000000011802001404000065000000140000202B000034A6010002000000000000000000000000000000005A000000188027101300486F617279204D617474656B617220526F626500000005179B1B0200000012000000A00F00000100000004000050007F0000007F0000003F010014050200"));
            // hoary in pack
            // Writer.Write(StringToByteArray("7D62B183110404017E00D40E2828B005500CB005740C8A04600C00D5037C1800D4037D1800B00B791800BE0C7818007F04000000011802001404000065000000140000202B000034A60100020000000000000000000000000000000007000000184025101300486F617279204D617474656B617220526F6265000000051739220200000012000000A00F00000100000004000050007F0000003F010014050200"));

        }

        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}