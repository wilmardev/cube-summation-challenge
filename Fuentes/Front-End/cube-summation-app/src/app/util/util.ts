import { RespuestaGeneral } from '../models/response-model';
import swal from 'sweetalert2';

export class Util {

    public baseUrl = 'http://localhost:9999/WebApiCodeSummation/api/';
    public MENSAJE_CAMPO_OBLIGATORIO = 'Campo Obligatorio';
    public MENSAJE_CAMPO_CARACTERES_INVALIDOS = 'No se permiten los caracteres: < , >';
    public MENSAJE_CAMPO_SOLO_LETRAS_ESPACIO = 'Solo se permiten letras y espacios';
    public MENSAJE_NO_RESULTADOS = 'No se encontraron resultados';

    constructor() { }

    manageResponseFalse(response: RespuestaGeneral) {
        this.showAlert(response.Mensaje, response);
    }

    manageResponseTrue(response: RespuestaGeneral) {
        this.showAlert('', response);
    }

    showAlert(message: string = '', response: RespuestaGeneral) {
        this.showSweetAlert(message, response.Estado);
    }

    showSweetAlert(message: string = '', status: boolean) {
        swal.fire({
            title: status ? 'Operación Exitosa' : 'Error',
            text: message,
            type: status ? 'success' : 'error',
            confirmButtonText: 'Aceptar',
            confirmButtonColor: '#007bff',
            allowOutsideClick: false,
            heightAuto: false
        });
    }
}

