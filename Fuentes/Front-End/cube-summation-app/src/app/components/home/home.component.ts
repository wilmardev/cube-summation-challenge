import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Util } from '../../util/util';
import { RespuestaGeneral } from '../../models/response-model';
import { CubeSummationService } from '../../services/cube-summation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  private DATA_INPUT_DEFAULT = `3
4 5
UPDATE 2 2 2 4
QUERY 1 1 1 3 3 3
UPDATE 1 1 1 23
QUERY 2 2 2 4 4 4
QUERY 1 1 1 3 3 3
2 4
UPDATE 2 2 2 1
QUERY 1 1 1 1 1 1
QUERY 1 1 1 2 2 2
QUERY 2 2 2 2 2 2`;
  formCubeSummation: FormGroup;
  formBuilder: FormBuilder = new FormBuilder();
  submitted = false;
  util: Util = new Util();
  @ViewChild('textDataInput') textAreaInput: ElementRef;
  @ViewChild('textDataOutput') textAreaOutput: ElementRef;

  constructor(private sendDataCube: CubeSummationService) { }

  ngOnInit() {
    this.CreateFormValidation();
  }

  get fo() { return this.formCubeSummation.controls; }

  CreateFormValidation() {
    this.formCubeSummation = this.formBuilder.group({
      dataInput: [this.DATA_INPUT_DEFAULT, [Validators.required]],
      dataOutput: ['', null]
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.formCubeSummation.valid) {
      this.sendDataCube.sendDataCube(this.formCubeSummation.value)
        .subscribe(response => {
          if (response.Estado) {
            this.actionAfterPostSuccess(response);
          } else {
            this.util.manageResponseFalse(response);
          }
        },
        error => { this.util.showSweetAlert('Ha ocurrido un error inesperado. Por favor contacte al personal de soporte.', false); });
    }
  }

  actionAfterPostSuccess(response: RespuestaGeneral): any {
    this.util.showSweetAlert(null, true);
    this.textAreaOutput.nativeElement.value = response.Mensaje;
  }

}
