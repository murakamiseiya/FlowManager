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

document.getElementById("button").onclick = function () {
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
    contents.setAttribute("id", "contents");
    contents.classList.add("col-lg-12");

    cell.appendChild(contents);

    //清算金額
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-1");
    cell.classList.add("active");

    var productionAmount = document.createElement("input");
    productionAmount.setAttribute("type", "text");
    productionAmount.setAttribute("id", "productionAmount");
    productionAmount.classList.add("col-lg-12");

    cell.appendChild(productionAmount);


    //申請種目
    cell = rows[0].insertCell(-1);
    cell.classList.add("col-lg-1");
    cell.classList.add("active");

    var AppLine = document.createElement("select");
    AppLine.setAttribute("id", "AppLine");
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

}

function coldel(obj) {
    // 削除ボタンを押下された行を取得
    tr = obj.parentNode.parentNode;
    // trのインデックスを取得して行を削除する
    tr.parentNode.deleteRow(tr.sectionRowIndex);
}