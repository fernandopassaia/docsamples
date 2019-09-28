//Design by Contract (Contracts): Is a concept to Validate Fields.
//To do That i create a Folder "Contracts" and then put all the Code There. The Contract is just a interface
//and it define what my Classes that will implement should have. So this class is like my "FluentValidator"
//in C#. It will have an "Errors" (Array) and a Method Validate that receive a Model "N" (some model) and
//will do all validations. With this Interface (Anotation By Fernando.)

export interface Contract {
    errors: any[]; //i could have "N" erros and show them at the same time for the user (ex: Email required and Street cannot be larger then 200)
    validate(model: any): boolean;
}