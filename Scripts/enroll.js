function addMoney() {
    var node_money = document.getElementById("money");
    var money = node_money.getAttribute("value") + "元";
    var nowmoney = document.getElementById("nowmoney");
    var nowmoney_text = document.createTextNode(money);
    nowmoney.appendChild(nowmoney_text);
    document.getElementById("money").onchange = function () {
        document.getElementById("nowmoney").textContent = this.value + "元";
    }
}
addMoney();

var node_select_image = document.getElementById("select_image");
var node_select_photo = document.getElementById("select_photo");
var node_photo = document.getElementById("photo");
node_select_image.addEventListener("click", function (e) {
    if (node_select_photo) {
        node_select_photo.click();
    }
    e.preventDefault();
}, false);

function headleFiles(files) {
    node_photo.setAttribute("src", window.URL.createObjectURL(files[0]));
    node_photo.onload() = function () {
        window.URL.revokeObjectURL(this.src);
    }
}

function exchangeCode() {
    var node_code = document.getElementById("verifycode_exchange");
    var array_code = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O',
        'P', 'A', 'S', 'D',
        'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M'
    ];
    var code = '';
    var percentage = Math.round(Math.random() * (100)) / 2 + 50 + "%";
    var bgc = 'background-color: rgb(' + percentage + "," + percentage + "," + percentage + ")";
    for (let i = 0; i < 4; ++i) {
        let random = array_code[Math.round(Math.random() * (array_code.length - 1))]
        code += random;
    }
    bgc += ";";
    node_code.textContent = code;
    node_code.setAttribute("style", bgc);
}

function selectCity() {
    var place = document.getElementById("nativeplace");
    var node_option = document.createElement("option");
    var node_city = document.getElementById("city");
    node_city.innerHTML = "";
    var city = new Array();
    city['江西省'] = ['南昌市', '九江市', '景德镇市', '鹰潭市', '新余市', '萍乡市', '赣州市', '上饶市', '抚州市', '宜春市', '吉安市'];
    city['河北省'] = ['石家庄市', '唐山市', '秦皇岛市', '邯郸市', '邢台市', '保定市', '张家口市', '承德市', '沧州市', '廊坊市', '衡水市'];
    city['山西省'] = ['太原市', '大同市', '阳泉市', '长治市', '晋城市', '朔州市', '晋中市', '运城市', '忻州市', '临汾市', '吕梁市'];
    city['湖北省'] = ['武汉市', '十堰市', '襄阳市', '荆门市', '孝感市', '黄冈市', '鄂州市', '黄石市', '咸宁市', '荆州市', '宜昌市', '随州市'];
    for (let i = 0; i < city[place.value].length; ++i) {
        var node_option = document.createElement("option");
        var node_option_text = document.createTextNode(city[place.value][i]);
        node_option.appendChild(node_option_text);
        node_option.setAttribute("value", city[place.value][i]);
        node_city.appendChild(node_option);
    }
}
selectCity();

document.getElementById("form").onsubmit = function() {
    var hobbys = document.getElementsByName("hobbys")[0];
    var hobby = document.getElementsByName("hobby");
    var str = "";
    for(let i =0; i <hobby.length ; ++i) {
        if (!hobby[i].checked) {
            continue;
        }
        i == (hobby.length - 1) ? (str += hobby[i].value) : (str += hobby[i].value + " ");
    }
    hobbys.value = str;
    // hobbys.setAttribute("value")
    console.log(str);
    return false;
}