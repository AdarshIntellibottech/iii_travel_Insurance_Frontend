import { useEffect, useState } from "react";
import { useSelector } from "react-redux";

const useFetch = (url, codeType) => {
  const [data, setdata] = useState(null);
  const [loading, setloading] = useState(true);
  const token = useSelector((state) => state.gatewayToken);
  const code = useSelector((state) => state.grandCode);

  useEffect(() => {
    const getCodeTable = async () => {
      const codeHeader = new Headers();
      codeHeader.append("Authorization", `Bearer ${token.data}`);
      codeHeader.append("grantCode", code.data);
      codeHeader.append("Content-Type", "application/json");

      var raw = JSON.stringify({
        codeTableName: codeType,
        insurerTenantCode: "III_SG",
        language: "en_US",
        prdtCode: "PTJ",
      });

      var requestOptions = {
        method: "POST",
        headers: codeHeader,
        body: raw,
        redirect: "follow",
      };

      const response = await fetch(
        `${process.env.NEXT_PUBLIC_GRANTCODE_API_URL}${url}`,
        requestOptions
      );
      const resData = await response.json();
      setdata(resData);
    };
    getCodeTable();
  }, [url, codeType, token, code]);
  return { data, loading };
};
export default useFetch;
