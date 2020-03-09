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
        /// <returns>文字の長さが指定範囲内であれば、true それ以外はfalseを返します。</returns>
        protected bool StringLengthCheck(String checkString , int maxLength , int minLength )
        {
            //チェックする文字列の長さ
            int checkStringLength = checkString.Length;

            //長さのチェック（下限）
            if(checkStringLength < minLength)
            {
                //検索文字列が下限より低い場合
                return false;
            }

            //長さのチェック（上限）
            if(checkStringLength > maxLength)
            {
                //検索文字列が上限より高い場合
                return false;

            }

            //長さが範囲内の場合
            return true;
        }

        /// <summary>
        /// 文字列の中にデータがあるかどうかのチェックを行います。
        /// </summary>
        /// <param name="checkString">検査文字列</param>
        /// <returns>データがあればtrue 、なければfalse</returns>
        protected bool StringRequiredCheck(String checkString)
        {
            //checkStringがなかった場合
            if(checkString == null)
            {
                return false;
            }
            //chekcStringがあった場合
            return true;
        }

        /// <summary>
        /// 禁則文字チェック
        /// </summary>
        /// <param name="checkString">検査文字列</param>
        /// <returns>禁則文字があればfalse、なければtrue</returns>
        protected bool StringProhidition(String checkString)
        {
            //文字が英数字の場合
            if (checkString.All(IsAlphanumeric))
            {
                return true;
            }

            //英数字以外の文字が入っていない
            return false;
        }

        /// <summary>
        /// 英数字のチェック
        /// </summary>
        /// <param name="checkChar">チェック文字</param>
        /// <returns>英数字であればtrue、それ以外の文字であればfalse</returns>
        private bool IsAlphanumeric(Char checkChar)
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

    }
}