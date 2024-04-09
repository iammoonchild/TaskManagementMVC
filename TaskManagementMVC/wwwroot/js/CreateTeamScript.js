
document.getElementById('teamMembersContainer').addEventListener('click', function (event) {
    // Check if the clicked element is a delete button
    if (event.target.classList.contains('delete-member')) {
        // Remove the closest wrapper div
        var memberWrappers = document.querySelectorAll('.team-member-wrapper');
        if (memberWrappers.length > 1) {
            event.target.closest('.team-member-wrapper').remove();
        }
    }
});

document.getElementById('addMore').addEventListener('click', function () {
    var container = document.getElementById('teamMembersContainer');
    var template = document.createElement('div');
    template.innerHTML = `
    <hr>
        <div class="team-member-wrapper">
            <div class="d-flex mb-2 ps-1 justify-content-between align-items-center teamMember-name-delete">
                <h5>Team Member</h5>
                <button type="button" class="btn btn-danger delete-member"><i class="bi bi-trash"></i></button>
            </div>
            <div class="mb-3 form-floating">
                <input type="text" class="form-control" name="FirstName[]" placeholder="" required>
                    <label for="FirstName[]">First Name</label>
            </div>
            <div class="mb-3 form-floating">
                <input type="text" class="form-control" name="LastName[]" placeholder="" required>
                    <label for="LastName[]">Last Name</label>
            </div>
            <div class="mb-3 form-floating">
                <input type="email" class="form-control" name="Email[]" placeholder="" required>
                    <label for="Email[]">Email ID</label>
            </div>
            <div class="mb-3 form-floating">
                <select class="form-select" name="Role[]" required>
                    <option value="">Select Role</option>
                    <option value="1">Team Lead</option>
                    <option value="2">Member</option>
                    <!-- Add more roles as needed -->
                </select>
                <label for="Role[]">Role</label>
            </div>
        </div>
                                `;
    container.appendChild(template);

    // Scroll to bottom of modal body
    var modalContent = document.querySelector('.modal-content');
    modalContent.scrollTop = modalContent.scrollHeight;
});

// Save team members
document.getElementById('saveTeamMembers').addEventListener('click', function () {
    var form = document.getElementById('teamMembersForm');
    var formData = new FormData(form);

    $.ajax({
        url: '/Manager/SetTeamMembersData',
        type: 'POST',
        data: formData, // Send FormData directly
        processData: false, // Prevent jQuery from processing the data
        contentType: false, // Prevent jQuery from setting contentType
        success: function (response) {
            alert('Team members saved successfully!');
        },
        error: function (error) {
            // Handle error
        }
    });
});
