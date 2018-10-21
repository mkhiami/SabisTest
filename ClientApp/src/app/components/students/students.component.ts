import { Component, OnInit } from '@angular/core';
import { BackendService } from '../../services/backendservice.service';
import { Student } from '../../models/student';
import { Course } from '../../models/course';
import { Semester } from '../../models/semester';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {

  constructor(private backendService: BackendService) { }
      public students: Student[];
    public courses: Course[];
    public semesters: Semester[];
    ngOnInit() {
         this.backendService.getStudents()
      .subscribe(success => {
        if (success) {
          this.students = this.backendService.students;
        }
      });
                 this.backendService.getSemesters()
      .subscribe(success => {
        if (success) {
          this.semesters = this.backendService.semesters;
        }
      });
              this.backendService.getCourses()
      .subscribe(success => {
        if (success) {
          this.courses = this.backendService.courses;
        }
      });
  } // end on init

}
