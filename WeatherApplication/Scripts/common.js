$(document).ready(function () {
    $("#txtCountry").keypress(function (e) {
        if (e.which == 13) {
            var countryName = $('#txtCountry').val();

            if (countryName.trim().length == 0) {
                alert("Please enter Country name and press enter key to get city list");
                return;
            }

            $.ajax({
                url: 'http://localhost:61394/api/Country/CitiesByCountry/' + countryName,
                type: 'GET',
                dataType: 'json',
                data: countryName,
                success: function (data, textStatus, xhr) {
                    var jsonData = JSON.stringify(data);
                    var lstCity = $('#lstCity');
                    lstCity.empty();

                    $.each(data, function () {
                        lstCity.append($("<option/>").val(this.Name).text(this.Name));
                    });
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert("Error Occurred!")
                    console.log(JSON.stringify(textStatus, null, 4));
                }
            });
        }
    });

    $("#btnGetWeather").click(function (e) {
        var countryName = $('#txtCountry').val();
        var cityName = $('#lstCity').val();

        $.ajax({
            url: 'http://localhost:61394/api/Weather/' + countryName + '/' + cityName,
            type: 'GET',
            dataType: 'json',            
            success: function (data, textStatus, xhr) {
                $('label').empty();

                var lblLong = $('#lblLong');
                lblLong.append(data.coord.lon);

                var lblLat = $('#lblLat');
                lblLat.append(data.coord.lat);

                var lblTime = $('#lblTime');
                lblTime.append(formatTime(data.dt));

                var lblWind = $('#lblWind');
                lblWind.append('Speed= ' + data.wind.speed + '  ' + data.wind.deg);

                var lblVisibility = $('#lblVisibility');
                lblVisibility.append(data.visibility)

                var lblSky = $('#lblSky');
                $.each(data.weather, function () {
                    lblSky.append(this.description)
                });

                var lblTemp = $("#lbltemp");
                lblTemp.append(data.main.temp);

                var tempMin = $('#tempMin');
                tempMin.append(data.main.temp_min);

                var tempMax = $('#tempMax');
                tempMax.append(data.main.temp_max);

                var lblHumidity = $('#lblHumidity');
                lblHumidity.append(data.main.humidity);

                var lblPressure = $('#lblPressure');
                lblPressure.append(data.main.pressure);
            },
            error: function (xhr, textStatus, errorThrown) {
                alert("Error Occurred!")
                console.log(JSON.stringify(textStatus, null, 4));
            }
        });
    });

    var formatTime = function (unixTimestamp) {
        var dt = new Date(unixTimestamp * 1000);

        var hours = dt.getHours();
        var minutes = dt.getMinutes();
        var seconds = dt.getSeconds();

        return hours + ":" + minutes + ":" + seconds;
    }

});