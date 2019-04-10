$(() => {
    //let id = $("#editid");
    $.get("/home/getpeople", function (ppl) {
        clearTable(ppl);
        ppl.forEach(person => AddPersonToTable(person));
    });

    $("#add-person").on('click', function () {

        let firstName = $("#firstname").val();
        let lastName = $("#lastname").val();
        let Age = $("#age").val();

        $.post("/home/addperson", { firstName, lastName, Age }, function (person) {
            person.FirstName = firstName;
            person.LastName = lastName;
            person.Age = Age;
        });

        $.get("/home/getpeople", function (ppl) {
            clearTable(ppl);
            ppl.forEach(person => AddPersonToTable(person));
        });

    });

    $("#people-table").on('click', '#edit', function () {
        const button = $(this);
        const id = button.data("id");
        const firstname = button.data("firstname");
        const lastname = button.data("lastname");
        const age = button.data("age");
        $("#firstname-modal").val(firstname);
        $("#lastname-modal").val(lastname);
        $("#age-modal").val(age);
        $("#editid").val(id);
        console.log(id);
        $("#edit-modal").modal();
    });

    $("#update").on('click', function () {
        const id = $("#editid").val();
        console.log(id);
        const FirstName = $("#firstname-modal").val();
        const LastName = $("#lastname-modal").val();
        const Age = $("#age-modal").val();

        $.post("/home/edit", { FirstName, LastName, Age, id }, function () {
            $.get("/home/getpeople", function (ppl) {
                clearTable(ppl);
                ppl.forEach(person => AddPersonToTable(person));
            });
        });

        $('#edit-modal').modal('hide');
    });
    $("#people-table").on('click', '#delete', function () {
        const button = $(this);
        const id = button.data("id");
        console.log(id);
        $.post("/home/delete", { id }, function (ppl) {
            $.get("/home/getpeople", function (ppl) {
                clearTable(ppl);
                ppl.forEach(person => AddPersonToTable(person));
            });
        });

    });
});


        const AddPersonToTable = person => {
            $("#people-table").append(`<tr>
                             <td>${person.FirstName}</td>
                             <td>${person.LastName}</td>
                             <td>${person.Age}</td>
                             <td><button class="btn btn-success" id="edit" data-id="${person.id}" data-firstname="${person.FirstName}" data-lastname="${person.LastName}" data-age="${person.Age}">Edit</button>   <button class="btn btn-danger" id="delete" data-id="${person.id}">Delete</button>
                           </tr>`);
        }

        function clearTable(people) {
            for (let i = 0; i <= people.length; i++) {
                $("#people-table").find("tr:gt(0)").remove();
            }
        }
   