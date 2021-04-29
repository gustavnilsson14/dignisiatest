class MockData{
  static debtWidgetMockData = (data) => {
    return {
  	    chart: {
  	        type: 'column'
  	    },
  	    title: {
  	        text: 'Debt Size',
            align:"left"
  	    },
  	    subtitle: {
  	        text: 'Each column shows the aggregated debt size for that month',
            align:"left"
  	    },
  	    xAxis: {
  	        categories: data.categories,
  	    },
  	    yAxis: {
  	        min: 0,
  	        title: {
  	            text: 'Principal Value'
  	        }
  	    },
        legend: {
            layout: 'horizontal',
            align: 'right',
            verticalAlign: 'top'
        },
  	    plotOptions: {
  	        column: {
  	            pointPadding: 0.2,
  	            borderWidth: 0
  	        }
  	    },
  	    series: data.series
  	};
  }

  static caseOverviewMockData = (data) => {
    return {
  	    title: {
  	        text: 'Case Overview',
            align:"left"
  	    },
  	    subtitle: {
  	        text: 'Compare open and closed cases',
            align:"left"
  	    },
        yAxis: {
            title: {
                text: 'Number of Employees'
            }
        },
        plotOptions: {
          line:{
            marker:{
              enabled:false
            }
          }
        },
        xAxis: {
            categories: data.categories
        },

        legend: {
            layout: 'horizontal',
            align: 'right',
            verticalAlign: 'top'
        },

        series: data.series
  	};
  }
}
export { MockData };
