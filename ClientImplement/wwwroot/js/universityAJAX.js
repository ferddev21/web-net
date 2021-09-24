$(document).ready(function () {
    var t = $('#myTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                exportOptions: {
                    columns: [1]
                },
                dom: {
                    className: 'btn btn-primary',
                }
            },
            {
                extend: 'pdf',
                exportOptions: {
                    columns: [1]
                },
                className: 'btn btn-primary',
            },
        ],
        "filter": true,
        "ajax": {
            "url": "/university",
            "datatype": "json",
            "dataSrc": "",
        },

        "order": [[1, 'asc']],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": [0, 2]
        }],
        "columns": [
            {
                "data": null
            },
            {
                "data": "name"
            },
            {
                "render": function (data, type, row, meta) {
                    var url_detail = `/university/${row["universityId"]}`;

                    var a =
                        `
                    <div class="float-right">
                        <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#modalEdit" onclick="editModalUniversity('${url_detail}')">
                            Edit
                        </button>
                        <button type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#modalEdit2" onclick="deleteModalUniversity('${row["universityId"]}')">
                            Delete
                        </button>
                    </div>
                    `
                    return a;
                }
            }
        ]
    });

    t.on('order.dt search.dt', function () {
        t.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

//create university
$("#form-univ").submit(function (event) {

    /* stop form from submitting normally */
    event.preventDefault();

    // var data_input = {
    //     "Name": $("#inputUniversity").val(),
    // }

    var data_input = new Object();
    data_input.Name = $("#inputUniversity").val();

    console.log(data_input);

    $.ajax({
        url: '/university',
        method: 'POST',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (data) {


            var obj = JSON.parse(data);

            console.log(obj);

            if (obj.errors != undefined) {

                document.getElementById("inputUniversity").className = "form-control is-invalid";
                $("#messageUniv").html(` ${obj.errors.Name}`);
            } else {
                //idmodal di hide
                $('#add').hide();
                $('.modal-backdrop').remove();


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `${obj.message}`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#myTable').DataTable().ajax.reload();

            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);

        }
    })
});

//Edit university
editModalUniversity = (url) => {
    $.ajax({
        url: url,
    }).done((result) => {
        // console.log(result.result.name);

        //set value
        $('#idUniv').val(`${result.universityId}`);
        $('#nameUniv').val(`${result.name}`);
        document.getElementById("nameUniv").className = "form-control";
    }).fail((result) => {
        console.log(result);
    });
}

//update university
$("#form-edit").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();


    var data_input = {
        "UniversityId": $("#idUniv").val(),
        "Name": $("#nameUniv").val(),
    }

    console.log(JSON.stringify(data_input));

    $.ajax({
        url: `/university`,
        method: 'PUT',
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded',
        data: data_input,
        success: function (data) {

            var obj = JSON.parse(data);

            console.log(obj);

            if (obj.errors != undefined) {

                document.getElementById("nameUniv").className = "form-control is-invalid";
                $("#messageUniv2").html(` ${obj.errors.Name}`);
            } else {
                //idmodal di hide
                $('#modalEdit').hide();
                $('.modal-backdrop').remove();


                //sweet alert message success
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: `Data berhasil update`,
                    showConfirmButton: false,
                    timer: 1500
                })

                //reload only datatable
                $('#myTable').DataTable().ajax.reload();

            }
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            document.getElementById("nameUniv").className = "form-control is-invalid";
            $("#messageUniv2").html(` ${err.errors.Name[0]}`);
        }
    });
});

//delete university
deleteModalUniversity = (id) => {

    console.log(id);

    Swal.fire({
        title: 'Hapus Data',
        text: `Anda akan menghapus data !`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete!'
    }).then((isDelete) => {
        if (isDelete.isConfirmed) {

            $.ajax({
                url: `/university/${id}`,
                method: 'DELETE',
                contentType: 'application/x-www-form-urlencoded',
                success: function (data) {

                    Swal.fire(
                        'Deleted!',
                        'Data berhasil dihapus.',
                        'success'
                    )

                    $('#myTable').DataTable().ajax.reload();
                },
            })
        }
    })
}