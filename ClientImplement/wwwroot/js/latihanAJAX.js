$.ajax({
    url: "https://swapi.dev/api/people/"
}).done((result) => {
    console.log(result);
    var text = '';
    $.each(result.results, function (key, val) {
        text += `
            <tr id="result${key + 1}">
                <td>${key + 1}</td>
                <td>${val.name}</td>
                <td>
                   <!-- Button trigger modal -->
                    <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal${key + 1}">
                    detail
                    </button>

                      <!-- <button type="button" class="btn btn-primary" data-toggle="modal" data-target="${val.name}" onclick="modal('${val.url}' )">
                    detail
                    </button>-->

                    <button  class="click${key + 1} btn btn-danger">delete</button>
                   
                    <!-- Modal -->
                    <div class="modal fade" id="exampleModal${key + 1}" tabindex="-1" aria-labelledby="exampleModal${key + 1}Label" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModal${key + 1}Label">Detail Of ${val.name}</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <ul>
                                        <li>gender : ${val.gender}</li>
                                        <li>height : ${val.height}</li>
                                        <li>hair color : ${val.hair_color}</li>
                                    </ul>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                </div>
                            </div>
                        </div>
                    </div>
                 
                </td>
            </tr>
        `;

        $(document).ready(function () {
            $(`.click${key + 1}`).click(function () {
                let isDelete = confirm(`Delete ${val.name} ?`);
                if (isDelete) {

                    var el = document.getElementById(`result${key + 1}`);
                    el.remove();
                }
            });
        });

    });

    $('#starwar').html(text);


}).fail((result) => {
    console.log(result);
});


modal = (url) => {
    $.ajax({
        url: url
    }).done((result) => {

        console.log(result.name);


    }).fail((result) => {
        console.log(result);
    });
};