
using System;
using System.Security.Cryptography;
using System.Text;

public class RSAGenerator
{
    public static void Main(string[] args)
    {
        // Gera o par de chaves RSA
        using (var rsa = new RSACryptoServiceProvider(2048))
        {
            try
            {
                // Exportar a chave privada no formato PEM compatível
                string privateKey = ExportPrivateKeyToPEM(rsa);

                // Exportar a chave pública no formato PEM compatível
                string publicKey = ExportPublicKeyToPEM(rsa);

                Console.WriteLine("Chave Privada:");
                Console.WriteLine(privateKey);
                Console.WriteLine();

                Console.WriteLine("Chave Pública:");
                Console.WriteLine(publicKey);
            }
            finally
            {
                rsa.PersistKeyInCsp = false; // Não persiste a chave
            }
        }
    }

    // Exportar chave privada no formato PEM (PKCS#1)
    public static string ExportPrivateKeyToPEM(RSACryptoServiceProvider rsa)
    {
        // Exportar parâmetros da chave privada
        var privateKeyParameters = rsa.ExportParameters(true);
        var privateKey = new StringBuilder();

        privateKey.AppendLine("-----BEGIN RSA PRIVATE KEY-----");
        privateKey.AppendLine(ConvertToBase64PEM(EncodePrivateKey(privateKeyParameters), 64));
        privateKey.AppendLine("-----END RSA PRIVATE KEY-----");

        return privateKey.ToString();
    }

    // Exportar chave pública no formato PEM (X.509)
    public static string ExportPublicKeyToPEM(RSACryptoServiceProvider rsa)
    {
        // Exportar parâmetros da chave pública
        var publicKeyParameters = rsa.ExportParameters(false);
        var publicKey = new StringBuilder();

        publicKey.AppendLine("-----BEGIN PUBLIC KEY-----");
        publicKey.AppendLine(ConvertToBase64PEM(EncodePublicKey(publicKeyParameters), 64));
        publicKey.AppendLine("-----END PUBLIC KEY-----");

        return publicKey.ToString();
    }

    // Codifica a chave privada usando o formato PKCS#1
    private static byte[] EncodePrivateKey(RSAParameters parameters)
    {
        using (var stream = new System.IO.MemoryStream())
        {
            using (var writer = new System.IO.BinaryWriter(stream))
            {
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new System.IO.MemoryStream())
                {
                    using (var innerWriter = new System.IO.BinaryWriter(innerStream))
                    {
                        innerWriter.Write((byte)0x02); // INTEGER (0)
                        WriteInteger(innerWriter, new byte[] { 0x00 });
                        WriteInteger(innerWriter, parameters.Modulus);       // n
                        WriteInteger(innerWriter, parameters.Exponent);      // e
                        WriteInteger(innerWriter, parameters.D);             // d
                        WriteInteger(innerWriter, parameters.P);             // p
                        WriteInteger(innerWriter, parameters.Q);             // q
                        WriteInteger(innerWriter, parameters.DP);            // dp
                        WriteInteger(innerWriter, parameters.DQ);            // dq
                        WriteInteger(innerWriter, parameters.InverseQ);      // iq
                        var length = (int)innerStream.Length;
                        writer.Write((byte)0x81); // Size of inner stream
                        writer.Write((byte)length);
                        writer.Write(innerStream.GetBuffer(), 0, length);
                    }
                }
            }
            return stream.ToArray();
        }
    }

    // Codifica a chave pública usando o formato X.509
    private static byte[] EncodePublicKey(RSAParameters parameters)
    {
        using (var stream = new System.IO.MemoryStream())
        {
            using (var writer = new System.IO.BinaryWriter(stream))
            {
                writer.Write((byte)0x30); // SEQUENCE
                using (var innerStream = new System.IO.MemoryStream())
                {
                    using (var innerWriter = new System.IO.BinaryWriter(innerStream))
                    {
                        innerWriter.Write((byte)0x02); // INTEGER
                        WriteInteger(innerWriter, parameters.Modulus);       // n
                        WriteInteger(innerWriter, parameters.Exponent);      // e
                        var length = (int)innerStream.Length;
                        writer.Write((byte)0x81); // Size of inner stream
                        writer.Write((byte)length);
                        writer.Write(innerStream.GetBuffer(), 0, length);
                    }
                }
            }
            return stream.ToArray();
        }
    }

    // Escreve os inteiros no formato ASN.1 DER
    private static void WriteInteger(System.IO.BinaryWriter writer, byte[] value)
    {
        writer.Write((byte)0x02); // INTEGER
        if (value[0] > 0x7F)
        {
            writer.Write((byte)(value.Length + 1));
            writer.Write((byte)0x00);
        }
        else
        {
            writer.Write((byte)value.Length);
        }
        writer.Write(value);
    }

    // Converte os bytes para Base64 no formato PEM com quebras de linha
    private static string ConvertToBase64PEM(byte[] data, int lineLength)
    {
        var base64 = Convert.ToBase64String(data);
        var sb = new StringBuilder();
        for (int i = 0; i < base64.Length; i += lineLength)
        {
            sb.AppendLine(base64.Substring(i, Math.Min(lineLength, base64.Length - i)));
        }
        return sb.ToString();
    }
}
