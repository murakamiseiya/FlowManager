///
/// ファイル名	: AppFormElementTable.js
/// 作成者		: murakami
/// 制作日		: 2020/3/12
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/12	新規作成
///

//テーブル詳細の数をカウント
var tableIndex = 0;

/// <summary>
/// 申請詳細の行を１行追加します。
/// </summary>
document.getElementById("button_Add").onclick = function () {
    // 表の作成開始
    var rows = [];
    var table = document.getElementById("AppFormTable");

    rows.push(table.insertRow(-1));  // 行の追加

    //申請内容
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-8");
    cell.classList.add("active");
    cell.colSpan = 2;

    var contents = document.createElement("input");
    contents.setAttribute("type", "text");
    contents.setAttribute("name", "AppFormDitails[" + tableIndex +"].Contents");
    contents.classList.add("col-lg-12");

    cell.appendChild(contents);

    //清算金額
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-1");
    cell.classList.add("active");

    var productionAmount = document.createElement("input");
    productionAmount.setAttribute("type", "text");
    productionAmount.setAttribute("name", "AppFormDitails[" + tableIndex +"].ProductionAmount");
    productionAmount.classList.add("col-lg-12");

    cell.appendChild(productionAmount);

    //申請種目
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-1");
    cell.classList.add("active");

    var AppLine = document.createElement("select");
    AppLine.setAttribute("name", "AppFormDitails[" + tableIndex +"].AppLine");
    AppLine.classList.add("col-lg-12");

    const optionValue = ["公共交通機関", "自家用車", "飲食代", "その他"];

    for (var i = 0; i < optionValue.length; i++) {
        var appLine_option = document.createElement("option");
        appLine_option.setAttribute("value", i);
        appLine_option.textContent = optionValue[i];
        AppLine.appendChild(appLine_option);
    }
    cell.appendChild(AppLine);

    //削除ボタン
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-1");
    cell.classList.add("active");

    var deleteButton = document.createElement("button");
    deleteButton.textContent = "×";
    deleteButton.setAttribute("onClick", "coldel(this)");
    cell.appendChild(deleteButton);

    //詳細の行の数を増やす
    tableIndex++;
}

/// <summary>
/// 申請詳細の行を１行削除します。
/// </summary>
function coldel(obj) {
    // 削除ボタンを押下された行を取得
    tr = obj.parentNode.parentNode;
    // trのインデックスを取得して行を削除する
    tr.parentNode.deleteRow(tr.sectionRowIndex);
    //詳細の行の数を減らす
    tableIndex--;
}

/// <summary>
/// formの送信処理を行います。
/// </summary>

document.getElementById("formSubmit").onclick = function () {
    console.log("formSubmit");
    $.post("https://localhost:44320/AppForm/SaveNewAppForm", $('form').serialize())
        .done(function (data, textStatus, jqXHR) {
            // 成功の場合の処理
            if (data == "true") {
                alert("登録成功です。");
            } else {
                alert("登録失敗です。");
            }
        });

}

