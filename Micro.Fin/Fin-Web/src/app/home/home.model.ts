export class HomeModel {

}

export class LoanModel {
    customerId: number;
    customerName: string;
    initialLoanAmount: number;
    dateGranted: Date;
    periodMonths: number;
    capitalOutstanding: number;
}

export class CreateLoanModel{
    customerId: number;
    initialLoanAmount: number;
    dateGranted: Date;
    periodMonths: number;
    interest: number;
    security: string;
    propertyValue: number;
    capitalOutstanding: number;
}
