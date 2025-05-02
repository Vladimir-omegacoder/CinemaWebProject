const selectedSeats = new Set();

function selectSeat(button) {
    const seatId = button.getAttribute('seat-id');

    if (selectedSeats.has(seatId)) {
        // Deselect the seat
        selectedSeats.delete(seatId);
        button.classList.remove('selected');
    } else {
        // Select the seat
        selectedSeats.add(seatId);
        button.classList.add('selected');
    }

    updateSubmitButton();
}

function updateSubmitButton() {
    const submitBtn = document.getElementById('submitBtn');
    const hiddenInput = document.getElementById('SelectedSeats');
    submitBtn.disabled = selectedSeats.size === 0;
    hiddenInput.value = Array.from(selectedSeats).join(',');
}