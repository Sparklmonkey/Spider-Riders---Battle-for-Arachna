﻿// Copyright (c) 2015 The original author or authors
//
// This software may be modified and distributed under the terms
// of the zlib license.  See the LICENSE file for details.

using SpriterDotNet.Helpers;
using System.Collections.Generic;
using UnityEngine;

namespace SpriterDotNet.Providers
{
    public class DefaultAssetProvider<T> : IAssetProvider<T>
    {
        public SpriterCharacterMap CharacterMap { get { return CharMaps.Count > 0 ? CharMaps.Peek() : null; } }

        public Dictionary<int, Dictionary<int, T>> AssetMappings { get; protected set; }

        protected Dictionary<T, T> SwappedAssets { get; set; }
        protected Dictionary<T, KeyValuePair<int, int>> CharMapValues { get; set; }
        protected Stack<SpriterCharacterMap> CharMaps { get; set; }

        public DefaultAssetProvider() : this(new Dictionary<int, Dictionary<int, T>>())
        {
        }

        public DefaultAssetProvider(Dictionary<int, Dictionary<int, T>> assetMappings)
        {
            AssetMappings = assetMappings;
            SwappedAssets = new Dictionary<T, T>();
            CharMapValues = new Dictionary<T, KeyValuePair<int, int>>();
            CharMaps = new Stack<SpriterCharacterMap>();
        }

        public virtual T Get(int folderId, int fileId)
        {
            T asset = GetAsset(folderId, fileId);
            if (asset == null) return asset;

            if (CharMapValues.ContainsKey(asset))
            {
                KeyValuePair<int, int> mapping = CharMapValues[asset];
                if (mapping.Key == folderId && mapping.Value == fileId) return asset;
                return Get(mapping.Key, mapping.Value);
            }

            return SwappedAssets.ContainsKey(asset) ? SwappedAssets[asset] : asset;
        }

        public virtual KeyValuePair<int, int> GetMapping(int folderId, int fileId)
        {
            T asset = GetAsset(folderId, fileId);
            if (asset == null || !CharMapValues.ContainsKey(asset)) return new KeyValuePair<int, int>(folderId, fileId);
            return CharMapValues[asset];
        }

        public virtual void Set(int folderId, int fileId, T asset)
        {
            Dictionary<int, T> objectsByFiles = AssetMappings.GetOrCreate(folderId);
            objectsByFiles[fileId] = asset;
        }

        public virtual void Swap(T original, T replacement)
        {
            SwappedAssets[original] = replacement;
        }

        public virtual void Unswap(T original)
        {
            if (SwappedAssets.ContainsKey(original)) SwappedAssets.Remove(original);
        }

        public virtual void PushCharMap(SpriterCharacterMap charMap)
        {
            ApplyCharMap(charMap);
            CharMaps.Push(charMap);
        }

        public virtual void PopCharMap()
        {
            if (CharMaps.Count == 0) return;
            CharMaps.Pop();
            ApplyCharMap(CharMaps.Count > 0 ? CharMaps.Peek() : null);
        }
        public virtual void PopCharMap(string mapName)
        {
            if (CharMaps.Count == 0) return;
            List<SpriterCharacterMap> tempList = new List<SpriterCharacterMap>(CharMaps.ToArray());
            for (int i = 0; i < tempList.Count; i++)
            {
                if(tempList[i].Name == mapName)
                {
                    tempList.RemoveAt(i);
                    ApplyCharMap(null);
                }
            }

            CharMaps = new Stack<SpriterCharacterMap>(tempList);
            foreach (var item in CharMaps)
            {
                ApplyCharMap(item);
            }
        }

        protected virtual void ApplyCharMap(SpriterCharacterMap charMap)
        {
            if (charMap == null)
            {
                CharMapValues.Clear();
                return;
            }

            for (int i = 0; i < charMap.Maps.Length; ++i)
            {
                SpriterMapInstruction map = charMap.Maps[i];
                T sprite = GetAsset(map.FolderId, map.FileId);
                if (sprite == null) continue;

                CharMapValues[sprite] = new KeyValuePair<int, int>(map.TargetFolderId, map.TargetFileId);
            }
        }

        protected virtual T GetAsset(int folderId, int fileId)
        {
            Dictionary<int, T> objectsByFiles;
            AssetMappings.TryGetValue(folderId, out objectsByFiles);
            if (objectsByFiles == null) return default(T);

            T obj;
            objectsByFiles.TryGetValue(fileId, out obj);

            return obj;
        }
    }
}