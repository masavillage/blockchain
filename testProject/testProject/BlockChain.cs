using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

// *************************************************
// ネットワークやノードは未実装
// *************************************************
namespace testProject
{
    /// <summary>
    /// ブロッククラス
    /// </summary>
    public class Block
    {
        /// <summary>
        /// インデックス
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// タイムスタンプ
        /// </summary>
        public DateTime TimesTamp { get; set; }

        /// <summary>
        /// トランザクション（取引データ）
        /// </summary>
        public List<Transaction> Transactions { get; set; }

        /// <summary>
        /// ノンス
        /// </summary>
        public int Nonce { get; set; }

        /// <summary>
        /// 前のブロックハッシュ
        /// </summary>
        public string PreviousHash { get; set; }

        public Block()
        {
            Transactions = new List<Transaction>();
        }
    }

    /// <summary>
    /// トランザクションクラス
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// 量
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 受信者
        /// </summary>
        public string Recipient { get; set; }

        /// <summary>
        /// 送信者
        /// </summary>
        public string Sender { get; set; }
    }

    public class BlockChain
    {
        /// <summary>
        /// ブロックチェーン用のリストを新規に生成する
        /// </summary>
        public List<Block> blockChain = new List<Block>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <remarks>
        /// コンストラクターはインスタンスを正しく初期化するための特別なメソッドです。
        /// コンストラクターは以下のように、クラス名と同じ名前のメソッドを書くことで定義できます。
        /// </remarks>
        public BlockChain()
        {
            // ジェネシスブロックの生成
            blockChain.Add(new Block()
            {
                Index = blockChain.Count,
                TimesTamp = DateTime.UtcNow,
                Nonce = 100,
                PreviousHash = "1"
            });
        }

        /// <summary>
        /// ブロック生成
        /// </summary>
        public void CreateBlock()
        {
            blockChain.Add(new Block
            {
                Index = blockChain.Count,
                TimesTamp = DateTime.UtcNow,
                Nonce = CreateNonce(blockChain.Last().Nonce),
                PreviousHash = CreateHash(blockChain.Last()),
            });
        }

        /// <summary>
        /// ノンス生成
        /// </summary>
        /// <param name="previousNonce"></param>
        /// <returns></returns>
        private int CreateNonce(int previousNonce)
        {
            int nonce = 0;
            while (!CheckNonce(previousNonce, nonce))
            {
                nonce++;
            }
            return nonce;
        }

        /// <summary>
        /// ナンスチェック
        /// </summary>
        /// <param name="previousNonce">前のブロックのナンス</param>
        /// <param name="nonce">チェックするナンス</param>
        /// <returns></returns>
        private bool CheckNonce(int previousNonce, int nonce)
        {
            // ハッシュ値の先頭2桁が「00」の場合はtrue、それ以外はfalseを返却する
            // ナンスとはnumber used onceの略で、一度きり使われる数を表す。
            // 主に暗号通信などの領域で使用されており、使い捨てのランダムな32ビットの値のことをさします
            // 先頭3桁にすると自PCでは全くハッシュ計算結果が想定通りにならないため、2桁でテスト。
            return GetHash($"{previousNonce}{nonce}").StartsWith("00");
        }

        /// <summary>
        /// ハッシュ値作成
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private string CreateHash(Block block)
        {
            // JsonConvert.SerializeObjectでオブジェクトを初期化する
            return GetHash(JsonConvert.SerializeObject(block));
        }

        /// <summary>
        /// ハッシュ値取得
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetHash(string data)
        {
            byte[] hash = null;
            var bytes = Encoding.Unicode.GetBytes(data);
            string JoinHash = string.Empty;
            using (var sha256 = new SHA256CryptoServiceProvider())
            {
                hash = sha256.ComputeHash(bytes);
            }

            JoinHash = string.Join("", hash.Select(x => x.ToString("X")));
            System.Console.WriteLine("JoinHash:" + JoinHash);
            return JoinHash;
        }
    }


}
