import React from 'react'
import { CompanyTenK } from '../../../company';
import { Link } from 'react-router-dom';

type Props = {
    tenK: CompanyTenK;
}

const TenKFinderItem = ({ tenK }: Props) => {
    const fillingDate = new Date(tenK.fillingDate).getFullYear();
    return (
      <Link
        reloadDocument
        to={tenK.finalLink}
        type="button"
        className="flex flex-col justify-center w-full px-6 py-3 bg-lightGreen border-2 border-Black-300 rounded-lg shadow-md cursor-pointer hover:opacity-70"
      >
        10K - {tenK.symbol} - {fillingDate}
      </Link>
    );
  };
  
export default TenKFinderItem