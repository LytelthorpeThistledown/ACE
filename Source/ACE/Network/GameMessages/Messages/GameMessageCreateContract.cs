using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Network.Enum;
using System;
using System.Linq;
using System.Numerics;

namespace ACE.Network.GameMessages.Messages
{
    public class GameMessageCreateContract : GameMessage
    {
        /// <summary>
        /// test creating stuff
        /// </summary>
        public GameMessageCreateContract(Player player) : base(GameMessageOpcode.ObjectCreate, GameMessageGroup.Group0A)
        {
            // Contract for Kill: Rynthid Rifts
            var bWeenie = WeenieHeaderFlag.Value | WeenieHeaderFlag.Usable | WeenieHeaderFlag.TargetType | WeenieHeaderFlag.UiEffects | WeenieHeaderFlag.StackSize | WeenieHeaderFlag.MaxStackSize | WeenieHeaderFlag.Container;
            var bWeenie2 = WeenieHeaderFlag2.Cooldown | WeenieHeaderFlag2.CooldownDuration;
            var bWO = new ImmutableWorldObject(ObjectType.Gem, new ObjectGuid(3695289433), "Contract for Kill: Rynthid Rifts", 51875,(ObjectDescriptionFlag)67108882, bWeenie, new Position(0, 0f, 0f, 0f, 0f, 0f, 0f, 0f));
            bWO.WeenieFlags2 = bWeenie2;

            var bPhysicsDesc = PhysicsDescriptionFlag.AnimationFrame | PhysicsDescriptionFlag.Stable | PhysicsDescriptionFlag.Petable | PhysicsDescriptionFlag.CSetup;
            bWO.PhysicsData.PhysicsDescriptionFlag = bPhysicsDesc;
            var bPhysicsState = (PhysicsState)1044; // TODO: fix this
            bWO.PhysicsData.PhysicsState = bPhysicsState;
            bWO.PhysicsData.AnimationFrameId = 101;
            bWO.PhysicsData.Stable = 536870932;
            bWO.PhysicsData.Petable = 872415275;
            bWO.PhysicsData.CSetup = 33554773;

            bWO.PhysicsData.PositionSequence = 2;
            bWO.PhysicsData.unknownseq0 = 0;
            bWO.PhysicsData.PhysicsSequence = 0;
            bWO.PhysicsData.JumpSequence = 0;
            bWO.PhysicsData.PortalSequence = 0;
            bWO.PhysicsData.unknownseq1 = 0;
            bWO.PhysicsData.SpawnSequence = 2;

            bWO.Icon = 100691928u;
            bWO.GameData.Value = 100;
            bWO.GameData.Usable = Usable.UsableContained;
            bWO.GameData.TargetType = (uint)ObjectType.Creature;
            bWO.GameData.UiEffects = (UiEffects)2; // TODO use UiEffects. maybe need to add an item?
            bWO.GameData.StackSize = 1;
            bWO.GameData.MaxStackSize = 1;
            bWO.GameData.ContainerId = player.Guid.Full;
            bWO.GameData.Cooldown = 100;
            bWO.GameData.CooldownDuration = 2;

           bWO.SerializeCreateObject(Writer);

            // NOT WORKING
            //Writer.Write(StringToByteArray("59A441DC11000000011820001404000065000000140000202B000034550100020100010001000100020001000000000002000000987008002000436F6E747261637420666F72204B696C6C3A2052796E7468696420526966747300000080A3CA0086D86F000800001200000406000000640000000800000010000000020000000100010004000050640000000000000000000040"));

            // working
            //Writer.Write(StringToByteArray("59A441DC11000000011802001404000065000000140000202B000034550100020200000000000000000000000000000002000000987008002000436F6E747261637420666F72204B696C6C3A2052796E7468696420526966747300000080A3CA0080D86F000800001200000406000000640000000800000010000000020000000100010004000050640000000000000000000040"));

           // Writer.Write(StringToByteArray("59A441DC11000000011802001404000065000000140000202B000034550100020200000000000000000000000000000002000000987008002000436F6E747261637420666F72204B696C6C3A2052796E7468696420526966747300000080A3CA0080D86F000800001200000406000000640000000800000010000000020000000100010004000050640000000000000000000040"));
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