import React, { useEffect, useState } from 'react'
import { CompanyBalanceSheet, CompanyCashFlow } from '../../company';
import { useOutletContext } from 'react-router';
import { getBalanceSheet } from '../../api';
import RatioList from '../RatioList/RatioList';

type Props = {}

const config = [
    {
      label: "Date",
      render: (company: CompanyCashFlow) => company.date,
    },
    {
      label: "Operating Cashflow",
      render: (company: CompanyCashFlow) => company.operatingCashFlow,
    },
    {
      label: "Investing Cashflow",
      render: (company: CompanyCashFlow) =>
        company.netCashUsedForInvestingActivites,
    },
    {
      label: "Financing Cashflow",
      render: (company: CompanyCashFlow) =>
        company.netCashUsedProvidedByFinancingActivities,
    },
    {
      label: "Cash At End of Period",
      render: (company: CompanyCashFlow) => company.cashAtEndOfPeriod,
    },
    {
      label: "CapEX",
      render: (company: CompanyCashFlow) => company.capitalExpenditure,
    },
    {
      label: "Issuance Of Stock",
      render: (company: CompanyCashFlow) => company.commonStockIssued,
    },
    {
      label: "Free Cash Flow",
      render: (company: CompanyCashFlow) => company.freeCashFlow,
    },
  ];
  

const BalanceSheet = (props: Props) => {
    const ticker = useOutletContext<string>();
    const [balanceSheet, setBalanceSheet] = useState<CompanyBalanceSheet>();

    useEffect(() => {
        const balanceSheetFetch = async () => {
            const result = await getBalanceSheet(ticker!);
            setBalanceSheet(result?.data[0]);
        };
        balanceSheetFetch();
    });

  return <>
    {
        balanceSheet ? (
            <RatioList config={config} data={balanceSheet}/>
        ):(
            <div>Company Not Found!</div>
        )
    }
  </>
}

export default BalanceSheet
