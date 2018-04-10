// Write your Javascript code.
$(document).ready(function() {
    getNotes();
})

$(document).on("submit", "#noteForm", function(e) {
    e.preventDefault();
    $("#error").html("");
    $.post("/newnote", $("#noteForm").serialize(), function(result) {
        if (result.failed) {
            $("#error").html("Title cannot be empty");
        } else {
            displayNote(result);
        }
    });
    $("#noteForm :input").val("");
})



function updateNote(noteId) {
    $.post("/updatenote", $(`#${noteId}`).serialize(), function() {
        getNotes();
    });
}

function deleteNote(noteId) {
    $.post(`/deletenote/${noteId}`, function() {
        getNotes();
    });
}

function getNotes() {
    $("#notes").text("");
    $.get("/notes", function(result) {
        for (note of result) {
            displayNote(note);
        }
    })
}

function displayNote(note) {
    $("#notes").prepend(`
                <div class="row">
                    <h2>${note.noteTitle}</h2>
                    <a onclick="deleteNote(${note.noteId})">delete</a>
                </div>
                <form id="${note.noteId}">
                    <input type="hidden" name="NoteId" value="${note.noteId}"/>
                    <textarea onchange="updateNote(${note.noteId})" name="NoteContent" placeholder="Enter description here...">${note.noteContent ? note.noteContent : ""}</textarea>
                </form>
                <hr/>
            `);
}