﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {

    var gyartoSelect = document.getElementById("gyartoSelect");
    var tipusSelect = document.getElementById("tipusSelect");

    if (gyartoSelect != null && tipusSelect != null) {
        if (gyartoSelect.value) {
            fetch(`/Advertisings/GetModels?manufacturer=${gyartoSelect.value}`)
                .then(response => response.json())
                .then(data => {
                    data.forEach(function (x) {
                        if (tipusSelect.value != x) {
                            var option = document.createElement("option");
                            option.value = x;
                            option.text = x;
                            tipusSelect.appendChild(option);
                        }
                    });
                })
                .catch(error => console.error("Hiba a modellek betöltésekor: ", error));
        }
    }
});

function changeModelValue() {

    var gyartoSelect = document.getElementById("gyartoSelect");
    var tipusSelect = document.getElementById("tipusSelect");

    tipusSelect.innerHTML = "";

    if (gyartoSelect.value) {
        fetch(`/Advertisings/GetModels?manufacturer=${gyartoSelect.value}`)
            .then(response => response.json())
            .then(data => {
                data.forEach(function (x) {
                    var option = document.createElement("option");
                    option.value = x;
                    option.text = x;
                    tipusSelect.appendChild(option);
                });
            })
            .catch(error => console.error("Hiba a modellek betöltésekor: ", error));
    }
}

function validateFields() {

    const minKm = parseInt(document.getElementById("min_km").value);
    const maxKm = parseInt(document.getElementById("max_km").value);

    const minYear = parseInt(document.getElementById("min_year").value);
    const maxYear = parseInt(document.getElementById("max_year").value);

    var btn = document.getElementById("search");

    if (document.getElementById("min_price") != null && document.getElementById("max_price") != null && document.getElementById("min_cylinder_capacity") != null && document.getElementById("max_cylinder_capacity") != null) {

        const minPrice = parseInt(document.getElementById("min_price").value);
        const maxPrice = parseInt(document.getElementById("max_price").value);

        const minCylinderCapacity = parseInt(document.getElementById("min_cylinder_capacity").value);
        const maxCylinderCapacity = parseInt(document.getElementById("max_cylinder_capacity").value);

        if ((!isNaN(minKm) && !isNaN(maxKm) && minKm > maxKm) ||
            (!isNaN(minYear) && !isNaN(maxYear) && minYear > maxYear) ||
            (!isNaN(minPrice) && !isNaN(maxPrice) && minPrice > maxPrice) ||
            (!isNaN(minCylinderCapacity) && !isNaN(maxCylinderCapacity) && minCylinderCapacity > maxCylinderCapacity)) {
            btn.disabled = true;
        } else {
            btn.disabled = false;
        }
    } else {
        if ((!isNaN(minKm) && !isNaN(maxKm) && minKm > maxKm) ||
            (!isNaN(minYear) && !isNaN(maxYear) && minYear > maxYear)) {
            btn.disabled = true;
        } else {
            btn.disabled = false;
        }
    }
}
