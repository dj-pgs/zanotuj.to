$(document).ready(function(){
	$('.selectize input').selectize({
    	plugins: ['remove_button'],
	    delimiter: ',',
	    copyClassesToDropdown: true,
	    persist: false,
	    create: function(input) {
	        return {
	            value: input,
	            text: input
	        }
	    }
	});

	$('[data-toggle="tooltip"]').tooltip()
});