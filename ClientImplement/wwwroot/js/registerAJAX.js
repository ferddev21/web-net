
$(document).ready(function () {
    var t = $('#myTable').DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'excel',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                dom: {
                    className: 'btn btn-primary',
                }
            },
            {
                extend: 'pdf',
                exportOptions: {
                    columns: [1, 2, 3, 4]
                },
                className: 'btn btn-primary',
            },
        ],
        "filter": true,
        "ajax": {
            "url": "/person/register",
            "datatype": "json",
            "dataSrc": "",
        },
        "order": [[1, 'asc']],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": [0, 5]
        }],
        "columns": [
            {
                "data": null
            },
            {
                "data": "nik"
            },
            {
                "data": "fullName",
                "autoWidth": true
            },
            {
                "data": "email"
            },
            {
                "render": function (data, type, row) {
                    return row["phone"].replace('0', '+62');
                },
            },
            {
                "render": function (data, type, row, meta) {
                    var url_detail = `/person/register/${row["nik"]}`;

                    var a =
                        `
                    <button type="button" class="btn btn-success btn-sm" data-toggle="modal" data-target="#detailPerson" onclick="detailModalPerson('${url_detail}')">
                        Detail
                    </button>
                    <button type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#detailPerson2" onclick="deleteModalPerson('${url_detail}')">
                        Delete
                    </button>
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


detailModalPerson = (url) => {
    $.ajax({
        url: url,
    }).done((result) => {
        console.log(result);

        $('#modal-title').html(`${result.fullName}`);
        $('#getNIK').html(`${result.nik}`);
        $('#getFirstName').html(`${result.firstName}`);
        $('#getLastName').html(`${result.lastName}`);
        $('#getGender').html((`${result.gender}`) == 1 ? "Laki-laki" : "Perempuan");
        $('#getDegree').html(`${result.degree}`);
        $('#getGPA').html(`${result.gpa}`);
        $('#getSalary').html(`Rp. ${result.salary}`);
        $.ajax({
            url: `/university/${result.universityId}`,
        }).done((result) => {
            $('#getUniv').html(`${result.name}`);
        });

        var text = '';
        $.each(result.accountRoles, function (key, val) {

            $.ajax({
                url: `http://localhost:5002/api/role/${val.roleId}`,
            }).done((result) => {
                // console.log(result.result.name);
                text += `<span class="badge badge-info font-weight-light mr-1">${result.name}</span>`;
                $('#getRole').html(text);
            });
        });


    }).fail((result) => {
        console.log(result);
    });
}

//form university
$.ajax({
    url: "http://localhost:5002/api/university",
}).done((result) => {
    console.log(result.result);
    text = '<option value=""></option>';
    $.each(result.result, function (key, val) {

        text += `<option value= "${val.universityId}"> ${val.name}</option> `;

    });

    $('#inputUniversity').html(text);

}).fail((result) => {
    console.log(result);
});

//new register
$("#form-register").submit(function (event) {


    /* stop form from submitting normally */
    event.preventDefault();

    gender = $('input[name="inputGender"]:checked').val();
    console.log(gender);


    var obj = new Object();
    obj.NIK = $("#inputNIK").val();
    obj.FirstName = $("#inputFirstName").val();
    obj.LastName = $("#inputLastName").val();
    obj.Phone = $("#inputPhone").val();
    obj.Email = $("#inputEmail").val();
    obj.Password = $("#inputPassword").val();
    obj.Degree = $("#inputDegree").val();
    obj.GPA = $("#inputGPA").val();


    if ($("#inputBirthDate").val() == "") {
        document.getElementById("inputBirthDate").className = "form-control is-invalid";
        $("#msgBirthDate").html(`BirthDate tidak boleh kosong`);
    } else {
        document.getElementById("inputBirthDate").className = "form-control is-valid";
        obj.BirthDate = $("#inputBirthDate").val();
    }

    if ($("#inputSalary").val() == "") {
        document.getElementById("inputSalary").className = "form-control is-invalid";
        $("#msgSalary").html(`Salary tidak boleh kosong`);
    } else {
        document.getElementById("inputSalary").className = "form-control is-valid";
        obj.Salary = $("#inputSalary").val();
    }

    if ($("#inputUniversity").val() == "") {
        document.getElementById("inputUniversity").className = "form-control is-invalid";
        $("#msgUniversity").html(`University tidak boleh kosong`);
    } else {
        document.getElementById("inputUniversity").className = "form-control is-valid";
        obj.UniversityId = parseInt($("#inputUniversity").val());
    }

    if (gender == null) {
        document.getElementsByClassName("inputGender").className = "form-check-input is-invalid";
        $("#msgGender").html(`Gender tidak boleh kosong`);
    } else {
        document.getElementsByClassName("inputGender").className = "form-check-input is-valid";
        obj.Gender = gender;
    }


    console.log(JSON.stringify(obj));
    //console.log(JSON.stringify(data_register));

    $.ajax({
        url: 'http://localhost:5002/api/person/register',
        type: 'post',
        dataType: 'json',
        contentType: 'application/json',
        data: JSON.stringify(obj),
        success: function (data) {
            console.log(data);

            //idmodal di hide
            $('#register').hide();
            $('.modal-backdrop').remove();

            //sweet alert message success
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: `${data.message}`,
                showConfirmButton: false,
                timer: 1500
            })

            //reload only datatable
            $('#myTable').DataTable().ajax.reload();
        },
        error: function (xhr, status, error) {
            var err = eval(xhr.responseJSON);
            console.log(err.message);
            if (err.errors != undefined) {
                checkValidation(err.errors.NIK, "inputNIK", "msgNIK");
                checkValidation(err.errors.FirstName, "inputFirstName", "msgFN");
                checkValidation(err.errors.LastName, "inputLastName", "msgLN");
                checkValidation(err.errors.Phone, "inputPhone", "msgPhone");
                checkValidation(err.errors.Email, "inputEmail", "msgEmail");
                checkValidation(err.errors.Password, "inputPassword", "msgPassword");
                checkValidation(err.errors.Degree, "inputDegree", "msgDegree");
                checkValidation(err.errors.GPA, "inputGPA", "msgGPA");
            } else {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: `${err.message}`,
                })
            }
        }
    })
});


function checkValidation(errorMsg, elementById, elementMsg) {
    if (errorMsg != undefined) {
        document.getElementById(`${elementById}`).className = "form-control is-invalid";
        $(`#${elementMsg}`).html(` ${errorMsg}`);
    } else {
        document.getElementById(`${elementById}`).className = "form-control is-valid";
    }
}


//delete Person
deleteModalPerson = (url) => {
    $.ajax({
        url: url,
    }).done((result) => {
        console.log(result.result.nik);

        Swal.fire({
            title: 'Hapus Data',
            text: `Anda akan menghapus data ${result.result.nik} !`,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete!'
        }).then((isDelete) => {
            if (isDelete.isConfirmed) {

                $.ajax({
                    url: `http://localhost:5002/api/person/${result.result.nik}`,
                    method: 'DELETE',
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

    }).fail((result) => {
        console.log(result);
    });
}