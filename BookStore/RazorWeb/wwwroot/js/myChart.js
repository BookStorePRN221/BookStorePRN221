var xValues = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
var value10 = [${ requestScope.TOTAL_IN_10 }]
var yValues = [120, 109, 30, 50, 207, 70, 120, 190, 300, value10, 200, 300]

const ctx = document.getElementById('myChart').getContext('2d');
const myChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: xValues,
        datasets: [{
            label: '# Statistics Total Order For 12 Months',
            data: yValues,
            backgroundColor: [
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
                '#267871',
            ],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
    }
});