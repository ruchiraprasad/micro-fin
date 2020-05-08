export class HomeModel {

}

export class LoanModel {
    id: number;
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

export class LoanDetailModel{
    id?: number;
    loanId: number;
    installment?: number;
    month?: Date;
    monthlyInterest?: number
    paid?: number
    latePaid?: number
    paidDate?: Date;
    capitalPaid?: number;
    balance?: number;
    interestType?: InterestType;
    isCompoundInterest?:boolean;
}

export enum InterestType{
    SimpleInterest = 0,
    CompoundInterest = 1
}
