using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Services.Core.Webapi.Redis
{
	/// <summary>
	/// 
	/// </summary>
	public static class IDistributedCacheExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
		where T : class, new()
		{
			using (var stream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(stream, value);
				await cache.SetAsync(key, stream.ToArray(), options);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="cache"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
		where T : class, new()
		{
			var data = await cache.GetAsync(key);
			using (var stream = new MemoryStream(data))
			{
				{
					return (T)new BinaryFormatter().Deserialize(stream);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cache"></param>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="options"></param>
		/// <returns></returns>
		public static async Task SetAsync(this IDistributedCache cache, string key, string value, DistributedCacheEntryOptions options)
		{
			await cache.SetAsync(key, Encoding.UTF8.GetBytes(value), options);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cache"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		public static async Task<string> GetAsync(this IDistributedCache cache, string key)
		{

			var data = await cache.GetAsync(key);
			if (data != null)
			{
				return Encoding.UTF8.GetString(data);
			}
			return null;
		}
	}
}
