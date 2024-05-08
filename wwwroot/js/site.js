// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#itemSelect").select2({
        ajax: {
            url: '/Home/GetItems',
            dataType: 'json',
            delay: 250,
            method:'POST',
            data: function (params) {
                return {
                    searchTerm: params.term || '',
                    pageSize: 10,
                    pageNumber: params.page || 1
                };
            },
            processResults: function (data, params) {
                params.page = params.page || 1;
                return {
                    results: data.items.map(item => ({ id: item.id, text: item.name })),
                    pagination: {
                        more: (params.page * 10) < data.total
                    }
                };
            },
            cache: true
        },
        placeholder: 'Select an item',
        minimumInputLength: 1
    });
});