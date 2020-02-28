

$(function () {
    $('.div-ajaxLoad').click(function (e) {
        //パスは アクション/クラス名
        $('.result').load($(this).data('category')+'/AppForm');
    });
});

