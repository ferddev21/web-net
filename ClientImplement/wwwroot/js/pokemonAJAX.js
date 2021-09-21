
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon",
}).done((result) => {
    console.log(result.results);
    text = '';
    $.each(result.results, function (key, val) {

        text += `
            <tr>
                <td>${key + 1}</td>
                <td>${val.name}</td>
                <td>
                    <button type="button" class="btn btn-success" data-toggle="modal" data-target="#exampleModal" onclick="detailModal('${val.url}')">
                        Detail
                    </button>
                </td>
            </tr>
        `;

    });

    $('#list_pokemon').html(text);

}).fail((result) => {
    console.log(result);
});

detailModal = (url) => {
    $.ajax({
        url: url,
    }).done((result) => {
        console.log(result);
        $('#model-poke').html(`${result.name}`);
        $('#img').html(`
         <img class="card-img-top border"
                                src="https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/dream-world/${result.id}.svg"
                                alt="Card image cap">
        `);
        text = '';
        $.each(result.abilities, function (key, val) {
            text += `
                <span class="badge badge-pill badge-success p-1">${val.ability.name}</span>
            `;
        });
        $('#badge-poke').html(text);

        $('#name-poke').html(`${result.name}`);
        $('#species-poke').html(`${result.species.name}`);

        // type = '';
        // $.each(result.types, function (key, val) {
        //     type += `
        //        ${type.name}
        //     `;
        // });
        // $('#type').html(type);

    }).fail((result) => {
        console.log(result);
    });
}

