using ACE.Network;
using ACE.Network.Enum;
using System.Collections.Generic;
using System.IO;

namespace ACE.Entity
{

    /// <summary>
    /// Segment to Control AC Model / Pallets and Textures
    /// </summary>
    public class ModelData
    {
        public uint PalleteGuid = 0;

        private List<ModelPallete> modelPalletes = new List<ModelPallete>();

        private List<ModelTexture> modelTextures = new List<ModelTexture>();

        private List<Model> models = new List<Model>();

        public void AddPallet(uint paletteId, byte offset, byte length)
        {
            ModelPallete newpallet = new ModelPallete(palleteID, offset,length);
            modelPalletes.Add(newpallet);
        }

        public void AddTexture(byte index, uint oldtexture, uint newtexture)
        {
            ModelTexture nextexture = new ModelTexture(index, oldtexture, newtexture);
            modelTextures.Add(nextexture);
        }

        public void AddModel(byte index, uint modelresourceid)
        {
            Model newmodel = new Model(index, modelresourceid);
            models.Add(newmodel);
        }

        //todo: render object network code
        public void Serialize(BinaryWriter writer)
        {
            writer.Write((byte)0x11);
            writer.Write((byte)modelPalletes.Count);
            writer.Write((byte)modelTextures.Count);
            writer.Write((byte)models.Count);

            if (modelPalletes.Count > 0)
                writer.Write((ushort)PalleteGuid);
            foreach (ModelPallete pallet in modelPalletes)
            {
                WorldObject.WritePackedDWord(pallet.PalletID, 0x4000000, writer);
                writer.Write((byte)pallet.Offset);
                writer.Write((byte)pallet.Length);

            }

            foreach (ModelTexture texture in modelTextures)
            {
                writer.Write((byte)texture.Index);
                WorldObject.WritePackedDWord(texture.OldTexture, 0x5000000, writer);
                WorldObject.WritePackedDWord(texture.NewTexture, 0x5000000, writer);
            }

            foreach (Model model in models)
            {
                writer.Write((byte)model.Index);
                WorldObject.WritePackedDWord(model.ModelID, 0x1000000, writer);
            }

            writer.Align();

        }

    }
}