using XSystem.Security.Cryptography;

namespace EntityFramework.Helpers;

public class GlobalMethods
{
	public static string ObtenerMd5(string valor)
	{
		MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
		byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);

		data = x.ComputeHash(data);

		string resp = "";
		for (int i = 0; i < data.Length; i++)
			resp += data[i].ToString("x2").ToLower();

		return resp;
	}
}
