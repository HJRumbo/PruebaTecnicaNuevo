import { Injectable } from '@angular/core';
import { AbstractControl, ValidationErrors, FormGroup } from '@angular/forms';
import { MyErrorStateMatcher } from '../class/myErrorStateMatcher';

@Injectable({
  providedIn: 'root'
})
export class ValidarService {

  miFormulario?: FormGroup;
  matcher: MyErrorStateMatcher;

  constructor() { 
    this.matcher = new MyErrorStateMatcher();
  }

  recibirFomulario(miFormulario: FormGroup) {
    this.miFormulario = miFormulario;
  }

  campoEsValido(campo:string): boolean | undefined{
    return this.miFormulario?.get(campo)?.invalid && 
      this.miFormulario?.get(campo)?.touched;
  }

  getMensaje(campo: string): string {
    const errorControl = this.miFormulario?.get(campo)?.errors;
    
    if (errorControl!['required']) return 'El campo es requerido';
    else if(errorControl!['ciudadesIguales']) return 'El origen y el destino no pueden ser los mismos.';
    else if(errorControl!['horasIguales']) return 'Las horas de salida y de llegada no pueden ser las mismas.';

    return '';
  }

  camposIguales( campo1: string, campo2: string ) {
    return ( formGroup: AbstractControl ): ValidationErrors | null => {

      const pass1 = formGroup.get(campo1)?.value;
      const pass2 = formGroup.get(campo2)?.value;

      if (pass1 != '' && pass2 != '') {
        if ( pass1 === pass2 ) {
          if (campo1 === 'ciudadOrigenId') {   
            formGroup.get(campo2)?.setErrors({ ciudadesIguales: true })
            return { ciudadesIguales: true };
          }else{
            formGroup.get(campo2)?.setErrors({ horasIguales: true })
            return { horasIguales: true };
          }
        }
        formGroup.get(campo2)?.setErrors(null)
      }
      return null;
    }
  } 
}
