
$(function(){
    console.log=function(){};
    var lat,
        lon,
        newLat,
        newLon, 
        city, 
        OWM_URL, 
        temp,
        humid,
        pressure,
        country,
        clouds,
        counter = 0;
        
    var $map = $("#map");
    var $spin = $(".spinner");
    var $show = $("#showMap");
    var $input = $("#input-city");
    var OWM_API_KEY = "f60e4a83f03cad255c90e70d343827fb";

    OMW_URL = "https://cors-anywhere.herokuapp.com/http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=metric&appid=" + OWM_API_KEY;

    currentCity();
    searchCity();
    toggleMap();
    function currentCity(){
        $.getJSON("https://ipinfo.io", callback);
        
        function callback(data) {
            lat = data.loc.split(',')[0];
            lon = data.loc.split(',')[1];
    
            OWM_URL = "https://cors-anywhere.herokuapp.com/http://api.openweathermap.org/data/2.5/weather?lat="+lat+"&lon="+lon+"&units=metric&appid=" + OWM_API_KEY;
            updateWeather(OWM_URL);
        }
    }
    
    function yourCity() {
        var city = $input.val();
        var urlParam = "";
        if (!city) {
            city="Split";
            urlParam = "q="+city;
        }
        var regEx = city.match(/\d+/g);
        if (regEx !== null) {
            urlParam = "zip="+city;
        }
        else {
            urlParam = "q="+city;
        }
        
        OWM_URL = "https://cors-anywhere.herokuapp.com/http://api.openweathermap.org/data/2.5/weather?"+urlParam+"&units=metric&appid=" + OWM_API_KEY;
        updateWeather(OWM_URL);
    }

    function updateWeather(OWM_URL) {
        $.getJSON(OWM_URL, function(weatherData){
                city = weatherData.name;
                country = weatherData.sys.country;
                windDir = degToCompass(weatherData.wind.deg % 360);
                humid = weatherData.main.humidity;
                pressure = weatherData.main.pressure;
                clouds = weatherData.clouds.all;
                
                $("body").css("background-image", "url(" + "./res/jnkfvnhuejcdferbvnd_" + weatherData.weather[0].icon + ".JPEG" + ")");
                $(".city").html(city + ", " + country);
                $(".pressure").html(pressure + "hpa");
                $(".humid").html(humid + "%");
                $(".condition-icon").attr("src", "./res/uelknvknjbbcbcdcbdjdcnun"+weatherData.weather[0].icon+".PNG");
                $(".weather-descr").html(firstLetterUpper(weatherData.weather[0].description) + "<br>(" + clouds + "% of clouds)");
                $(".container-fluid").fadeIn(800);
                $spin.hide();
                $(".compass").css({
                    "transform":"rotate(" + weatherData.wind.deg + "deg)",
                    "-webkit-transform":"rotate(" + weatherData.wind.deg + "deg)"
                });
                
                unitTrigger(weatherData);
                
                lat = weatherData.coord.lat;
                lon = weatherData.coord.lon;
                
                newLat = lat;
                newLon = lon;
                
        }).fail(function(error) {
            if (error.status == 404) {
                yourCity();
            }
            else {
                currentCity();
            }
        });
    }
    
    function updateUnits(weatherData) {
        
        temp = weatherData.main.temp;
        windSpeed = weatherData.wind.speed*3.6;
        
        if(counter % 2 == 1) {
            temp = +(temp * (9/5) + 32).toFixed(1) +"°F";
            windSpeed = +(windSpeed * 0.62137119223733).toFixed(2)+"mph";
            $("#change").val("°C");
        }
        else {
            temp = +temp.toFixed(1) +"°C";
            windSpeed = +windSpeed.toFixed(2)+"kph";
            $("#change").val("°F");
        }
        
        $(".temp").html(temp);
        if(windDir) {
            $(".wind").html(windDir + " " + windSpeed);
        }
        else {
            $(".wind").html("N " + windSpeed);
        }
    }
    
    function unitTrigger(data) {
        updateUnits(data);
        $("#change").off("click").on("click",function(){
            counter++;
            updateUnits(data);
        });
    }

    function degToCompass(num) {
        var val = Math.floor((num / 22.5) + 0.5);
        var arr = ["N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW"];
        return arr[(val % 16)];
    }
    
    function onEnterSearch() {
        $input.keyup(function(event) {
            if (event.keyCode == 13) {
                $("#submit").click();
            }
        });
    }
    
    function searchCity() {
        onEnterSearch();
        
        $("#submit").off('click').on('click',function() {
            $(".container-fluid").fadeOut(200);
            $spin.show();
            yourCity();
            $input.val('').blur();
        });
    }
    
    function firstLetterUpper(string) {
        return string.charAt(0).toUpperCase() + string.slice(1);
    }
});