import { HttpClient, HttpResponse, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs"
import { map } from 'rxjs/operators';
import { Course } from "../models/course";
import { Student } from "../models/student";
import { Semester } from "../models/semester";

@Injectable()
export class BackendService {

    public courses: Course[] = [];
    public semesters: Semester[] = [];
    public students: Student[] = [];
    public student: Student ;
  constructor(private http: HttpClient) {}

  getStudents(): Observable<boolean> {
    return this.http.get("/api/sabis/students")
      .pipe(
        map((data: any[]) => {
          this.students = data;
          return true;
      }))};
     getStudent(id): Observable<boolean> {
    return this.http.get("/api/sabis/student/"+id)
      .pipe(
        map((data: Student) => {
          this.student = data;
          return true;
      }))};
  getSemesters(): Observable<boolean> {
    return this.http.get("/api/sabis/semesters")
      .pipe(
        map((data: any[]) => {
          this.semesters = data;
          return true;
      }))};

  getCourses(): Observable<boolean> {
    return this.http.get("/api/sabis/courses")
      .pipe(
        map((data: any[]) => {
          this.courses = data;
          return true;
        }))};
}