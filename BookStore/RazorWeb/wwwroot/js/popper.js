// Ví dụ sử dụng Popper.js để tạo tooltip
const tooltipTrigger = document.getElementById("tooltip-trigger");
const tooltip = document.getElementById("tooltip");

new Popper(tooltipTrigger, tooltip, {
    placement: "top"
});