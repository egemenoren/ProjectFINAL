

$(document).ready(function () {

    var arrLang = {
        'tr':{
            'MENU_PLANT_NAME': 'Bitki Adı',
            'MENU_WATERING_OPTIONS': 'Sulama Ayarları'
        },
        'en': {
            'MENU_PLANT_NAME': 'Plant Name',
            'MENU_WATERING_OPTIONS': 'Watering Options'
        }
    }

    $(".language-selector").click(function () {
        localStorage.setItem('dil', JSON.stringify($(this).attr('id')));
        location.reload();
    });
    var lang = JSON.parse(localStorage.getItem('dil'));

    if (lang == "en") {
        $("#drop_yazi").html('English');
    }
    else
        $("#drop_yazi").html('Türkçe');

    $('a,h5,p,h1,h2,span,li,button,h3,label').each(function (index, element) {
        $(this).text(arrLang[lang][$(this).attr('key')]);
    })
})