///
/// ファイル名	: AppFormAjax.js
/// 作成者		: murakami
/// 制作日		: 2020/3/12
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/12	新規作成
///


$(function () {
    $('.div-ajaxLoad').click(function (e) {
        //パスは アクション/クラス名
        $('.result').load($(this).data('category')+'/AppForm');
    });
});

