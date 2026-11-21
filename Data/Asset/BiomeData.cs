using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Editor.Data.Asset
{
    public class BiomeData
    {
        string PackagePath;
        GameVersion BaseVersion;
        public Dictionary<AssetType, Dictionary<string, AssetData>> assetsData;
        public BiomeData()
        {
            assetsData = new Dictionary<AssetType, Dictionary<string, AssetData>>();
        }
        public List<AssetData> GetAssets()
        {
            List<AssetData> list = new List<AssetData>();
            foreach (var assetList in assetsData.Values)
            {
                list.AddRange(assetList.Values);
            }
            return list;
        }
        public List<AssetData> GetAssetsByType(AssetType type)
        {
            if (!assetsData.ContainsKey(type))
            {
                return new List<AssetData>();
            }
            return assetsData[type].Values.ToList();
        }
    }
}
