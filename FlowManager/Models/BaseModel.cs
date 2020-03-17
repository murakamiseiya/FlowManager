///
/// ファイル名	: BaseModel.cs
/// 作成者		: murakami
/// 制作日		: 2020/3/1
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/01	新規作成
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FlowManager.Models
{
    /// <summary>
    /// モデルの基底クラス
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 文字列の長さをチェックします。
        /// </summary>
        /// <param name="checkString">検査する文字列</param>
        /// <param name="maxLength">文字の最大数</param>
        /// <param name="minLength">文字の最小数</param>
        /// <returns>文字の長さが指定範囲内であれば、false それ以外はtrueを返します。</returns>
        protected bool StringLengthCheck(String checkString , int maxLength , int minLength )
        {
            //チェックする文字列の長さ
            int checkStringLength = checkString.Length;

            //長さのチェック（下限）
            if(checkStringLength < minLength)
            {
                //検索文字列が下限より低い場合
                return true;
            }

            //長さのチェック（上限）
            if(checkStringLength > maxLength)
            {
                //検索文字列が上限より高い場合
                return true;

            }

            //長さが範囲内の場合
            return false;
        }

        /// <summary>
        /// 文字列の中にデータがあるかどうかのチェックを行います。
        /// </summary>
        /// <param name="checkString">検査文字列</param>
        /// <returns>データがあればfalse 、なければtrue</returns>
        protected bool RequiredCheck(Object checkString)
        {
            //checkStringがなかった場合
            if(checkString == null)
            {
                return true;
            }
            //chekcStringがあった場合
            return false;
        }

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="checkString">検査文字列</param>
        /// <returns>禁則文字があればtrue、なければfalse</returns>
        protected bool StringProhidition(String checkString)
        {
            //文字が英数字の場合
            if (checkString.All(IsAlphanumeric))
            {
                return false;
            }

            //英数字以外の文字が入っていない
            return true;
        }

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="checkString">検査文字列</param>
        /// <returns>禁則文字があればtrue、なければfalse</returns>
        protected bool StringProhiditionTo2Byte(String checkString)
        {
            //文字が英数字、２バイトの場合
            if (checkString.All(zenkakuAndAlphanumeric))
            {
                return false;
            }

            //英数字以外の文字が入っていない
            return true;
        }

        /// <summary>
        /// 全角文字かどうかをチェックします。
        /// </summary>
        /// <param name="str">検査文字列</param>
        /// <returns>全角ならtrue、半角ならfalse</returns>
        public static bool isZenkaku(string str)
        {
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length * 2;
        }

        /// <summary>
        /// 全角文字、半角英数とそれ以外をチェックします。
        /// </summary>
        /// <param name="checkChar">チェック文字</param>
        /// <returns>英数字、2バイト文字であればtrue、それ以外の文字であればfalse</returns>
        protected bool zenkakuAndAlphanumeric(Char checkChar)
        {
            //全角文字であればOK
            if (isZenkaku(checkChar.ToString()))
            {
                return true;
            }
            //全角以外
            else
            {
                //半角英数字をチェック
                if (IsAlphanumeric(checkChar))
                {
                    return true;
                }
                return false; 
            }
        }

        /// <summary>
        /// 英数字のチェック
        /// </summary>
        /// <param name="checkChar">チェック文字</param>
        /// <returns>英数字であればfalse、それ以外の文字であればtrue</returns>
        protected bool IsAlphanumeric(Char checkChar)
        {
            //数字の場合
            if (char.IsDigit(checkChar))
            {
                return true;
            }

            //英字の場合
            if(checkChar >= 'A' && checkChar <= 'z')
            {
                return true;
            }

            //英数字以外の場合
            return false;
        }

        /// <summary>
        /// 数字の長さをチェックします。
        /// </summary>
        /// <param name="checkInt">検査する数字</param>
        /// <param name="minInt">最小値</param>
        /// <param name="maxInt">最大値</param>
        /// <returns>範囲外であればtrue,範囲内であればfalse</returns>
        protected bool IntSizeCheck(int checkInt,int minInt,int maxInt)
        {
            //マイナス値だった場合
            if( checkInt <= minInt)
            {
                return true;
            }

            //上限値を超えた場合
            if( checkInt >= maxInt)
            {
                return true;
            }

            //範囲内であれば
            return false;
        }

    }
}