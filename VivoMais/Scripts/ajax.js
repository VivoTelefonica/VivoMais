function FormataCpf(campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(campo.value);
    vr = vr.replace(".", "");
    vr = vr.replace("/", "");
    vr = vr.replace("-", "");
    tam = vr.length + 1;
    if (tecla != 14) {
        if (tam == 4)
            campo.value = vr.substr(0, 3) + '.';
        if (tam == 7)
            campo.value = vr.substr(0, 3) + '.' + vr.substr(3, 6) + '.';
        if (tam == 11)
            campo.value = vr.substr(0, 3) + '.' + vr.substr(3, 3) + '.' + vr.substr(7, 3) + '-' + vr.substr(11, 2);
    }
}

function formatoNumeroVirgula(Press) {
    /*=========================================
      onkeypress="return formatoNumero(event)"
     ========================================*/
    if ((Press.keyCode < 58 && Press.keyCode > 47) || Press.keyCode == 44) {
        return true;
    } else {
        return false;
    }
}

function Limpar(valor, validos) {
    // retira caracteres invalidos da string
    var result = "";
    var aux;
    for (var i = 0; i < valor.length; i++) {
        aux = validos.indexOf(valor.substring(i, i + 1));
        if (aux >= 0) {
            result += aux;
        }
    }
    return result;
}

function FormataNumero(campo, tammax, teclapres, decimal) {
    var tecla = teclapres.keyCode;
    vr = Limpar(campo.value, "0123456789");
    tam = vr.length;
    dec = decimal

    if (tam < tammax && tecla != 8) { tam = vr.length + 1; }

    if (tecla == 8)
    { tam = tam - 1; }

    if (tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105) {

        if (tam <= dec)
        { campo.value = vr; }

        if ((tam > dec) && (tam <= 5)) {
            campo.value = vr.substr(0, tam - 2) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 6) && (tam <= 8)) {
            campo.value = vr.substr(0, tam - 5) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 9) && (tam <= 11)) {
            campo.value = vr.substr(0, tam - 8) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 12) && (tam <= 14)) {
            campo.value = vr.substr(0, tam - 11) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - dec, tam);
        }
        if ((tam >= 15) && (tam <= 17)) {
            campo.value = vr.substr(0, tam - 14) + "." + vr.substr(tam - 14, 3) + "." + vr.substr(tam - 11, 3) + "." + vr.substr(tam - 8, 3) + "." + vr.substr(tam - 5, 3) + "," + vr.substr(tam - 2, tam);
        }
    }
}

function FormataCnpj(campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(campo.value);
    vr = vr.replace(".", "");
    vr = vr.replace("/", "");
    vr = vr.replace("-", "");
    tam = vr.length + 1;
    if (tecla != 14) {
        if (tam == 3)
            campo.value = vr.substr(0, 2) + '.';
        if (tam == 6)
            campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 5) + '.';
        if (tam == 10)
            campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(6, 3) + '/';
        if (tam == 15)
            campo.value = vr.substr(0, 2) + '.' + vr.substr(2, 3) + '.' + vr.substr(6, 3) + '/' + vr.substr(9, 4) + '-' + vr.substr(13, 2);
    }
}

function formatoNumero(Press) {
    /*=========================================
    onkeypress="return formatoNumero(event)"
    ========================================*/
    if (Press.keyCode < 58 && Press.keyCode > 47) {
        return true;
    } else {
        return false;
    }
}

function CompletaData(strValor) {

    if ((event.keyCode < 48) | (event.keyCode > 57) && (event.keyCode != 13))
        event.keyCode = 0;

    if ((strValor.length == 2) || (strValor.length == 5)) {
        strValor += '/';
    }
    return strValor;
}

function formatarMascara(mascara, documento) {
    var i = documento.value.length;
    var saida = mascara.substring(0, 1);
    var texto = mascara.substring(i)

    if (texto.substring(0, 1) != saida) {
        documento.value += texto.substring(0, 1);
    }

}