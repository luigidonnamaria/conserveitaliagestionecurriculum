//gestione responsive tab
    $.fn.responsiveTabs = function () {

        this.addClass('responsive-tabs');
            this.append($('<span class="glyphicon glyphicon-triangle-bottom"></span>'));
            this.append($('<span class="glyphicon glyphicon-triangle-top"></span>'));

            this.on('click', 'li.active > button, span.glyphicon', function () {
        this.toggleClass('open');
            }.bind(this));

            this.on('click', 'li:not(.active) > button', function () {
        this.removeClass('open');
            }.bind(this));
        };

        $('.nav.nav-tabs').responsiveTabs();

//validazione campi
function validate() {

	var valid = true;
	validNome = checkEmpty($("#nome"));
	validCognome = checkEmpty($("#cognome"));
	validEmail = checkEmail($("#email"));
	validIndirizzo = checkEmpty($("#indirizzo"));
	validCitta = checkEmpty($("#città"));
	validProvincia = checkEmpty($("#provincia"));
	validCap = checkCap($("#cap"));
	validStato = checkEmpty($("#stato"));
	validSesso = checkSesso($("input[name='curriculum.Sesso']"));
	validStatoCivile = checkEmpty($("#statocivile"));
	validTelefonoAbitazione = checkEmpty($("#telefonoabitazione"));
	validDataNascita = checkEmpty($("#datanascita"));
	validNazionalita = checkEmpty($("#nazionalità"));
	validTitoloStudio = checkEmpty($("#titolostudio"));
	validSede = checkEmpty($("#sede"));
	validAnno = checkNumber($("#anno"));
	validTipologia = checkEmpty($("#tipologia"));
	validVoto = checkNumber($("#voto"));
	validMadrelingua = checkEmpty($("#madrelingua"));
	validAreaPrioritaria = checkEmpty($("#areaprioritaria"));
	validTermini = checkTermini($("#terminirisp"));
	valid = validNome && validCognome && validIndirizzo &&  validCitta && validProvincia && validCap && validStato && validSesso && validStatoCivile &&  validTelefonoAbitazione && validDataNascita && validNazionalita && validTitoloStudio && validSede && validEmail && validAnno && validTipologia && validVoto && validMadrelingua && validAreaPrioritaria && validTermini;
	
	$("#submit-btn").attr("disabled", true);
	$("#validationError").show();
	if (valid) {
		$("#submit-btn").attr("disabled", false);
		$("#validationError").hide();
	}
	
}
function checkEmpty(obj) {
	var name = $(obj).attr("id");
	
	$("." + name + "-validation").html("");
	$(obj).css("border", "");
	
	if ($(obj).val() === "") {
		
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Campo obbligatorio");
		return false;
	}
	console.log("Checkempty->ReturnTreu->" + name);
	return true;
}
function checkEmail(obj) {
	var result = true;
	
	var name = $(obj).attr("id");
	
	$("." + name + "-validation").html("");
	$(obj).css("border", "");

	result = checkEmpty(obj);

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Campo obbligatorio.");
		return false;
	}

	var email_regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,3})+$/;
	result = email_regex.test($(obj).val());

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Inserire formato valido");
		return false;
	}
	
	return result;
}

function checkCap(obj) {
	var result = true;

	var name = $(obj).attr("id");
	
	$("." + name + "-validation").html("");
	$(obj).css("border", "");

	result = checkEmpty(obj);

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Campo obbligatorio");
		return false;
	}

	var cap_regex = /^[/\d]{5}?$/;
	result = cap_regex.test($(obj).val());

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Inserire formato valido di 5 cifre");
		return false;
	}
	
	return result;
}

function checkNumber(obj) {
	var result = true;

	var name = $(obj).attr("id");
	
	$("." + name + "-validation").html("");
	$(obj).css("border", "");

	result = checkEmpty(obj);

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Campo obbligatorio");
		return false;
	}

	var cap_regex = /^[0-9]+$/;
	result = cap_regex.test($(obj).val());

	if (!result) {
		$(obj).css("border", "#FF0000 1px solid");
		$("." + name + "-validation").html("Inserire valori numerici");
		return false;
	}
	
	return result;
}

function checkSesso(obj) {
	
	$(".sesso-validation").html("");
	
	
	if (!$(obj).is(":checked")) {
		
		
		$(".sesso-validation").html("Campo obbligatorio.");
		return false;
	}
	
	return true;
}

function checkTermini(obj) {

	$(".termini-validation").html("");

	
	if (!$(obj).is(":checked")) {


		$(".termini-validation").html("L'informativa sulla privacy deve essere accettata.");
		return false;
	}

	return true;
}
//settaggio calendario in italiano
document.addEventListener('DOMContentLoaded', function () {
	datanascita = document.getElementById('datanascita').ej2_instances[0];
	occupatodal = document.getElementById('occupatodal').ej2_instances[0];
	scadenza = document.getElementById('scadenza').ej2_instances[0];
	
	loadCultureFiles('it');
	datanascita.locale = 'it';
	occupatodal.locale = 'it';
	scadenza.locale = 'it';

});

function loadCultureFiles(name) {
	var files = ['ca-gregorian.json', 'numbers.json', 'timeZoneNames.json'];
	if (name === 'ar') {
		files.push('numberingSystems.json');
	}
	var loader = ej.base.loadCldr;
	var loadCulture = function (prop) {
		var val, ajax;

		ajax = new ej.base.Ajax(location.origin + location.pathname + '/../../script/cldr-data/' + name + '/' + files[prop], 'GET', false);

		ajax.onSuccess = function (value) {
			val = value;
		};
		ajax.send();
		loader(JSON.parse(val));
	};
	for (var prop = 0; prop < files.length; prop++) {
		loadCulture(prop);
	}
};

//check file extension
$(document).ready(function () {
	$("#submit-btn").attr("disabled", true);
	$("#uploadfile").change(function () {
		$(".upload-validation").html("");		
		var fileExtension = ['doc', 'docx', 'txt', 'pdf'];
		if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) === -1) {
			$(this).val('');
			$(".upload-validation").html("Sono consentiti solo i seguenti formati: "+ fileExtension.join(', '));
		}
	});
	$('#submit-btn').click(function () {
		return validate();
	});
});









       
       





    