import { Component, OnInit } from '@angular/core';
import { Student } from '../../models/student';
import { BackendService } from '../../services/backendservice.service';
import { ActivatedRoute } from '../../../../node_modules/@angular/router';

@Component({
  selector: 'app-student',
  templateUrl: './student.component.html',
  styleUrls: ['./student.component.css']
})
export class StudentComponent implements OnInit {

  constructor(private backendService : BackendService, private route: ActivatedRoute) { }
    public student: Student;
    id: string = '';

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
      console.log(params);
      this.id =this.route.snapshot.params.id;
        });
        if(this.id != ''){
        this.backendService.getStudent(this.id)
        .subscribe(success => {
            if (success) {
               // alert('retrieved student');
          this.student = this.backendService.student;
        }
      });
        }//end id present
  }//end on init
    saveStudent(){
    alert(JSON.stringify(this.student));
    }
}
