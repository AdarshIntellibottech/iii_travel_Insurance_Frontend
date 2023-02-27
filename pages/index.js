import Accordion from "@/components/Accordion";
import Topbar from "@/components/Topbar";
import Head from "next/head";
import Image from "next/image";
import { useEffect } from "react";
import { useDispatch } from "react-redux"
import { setToken } from "../store/gatewayTokenSlice"
import { setGrantCode } from "@/store/grandCodeSlice";
import Footer from "@/components/Footer";

export default function Home({res}) {

  const dispatch = useDispatch()

  useEffect(() => {
    dispatch(setToken(res.access_token))

    const getGrantCode = async() => {

      const grantCodeRaw = JSON.stringify({
        "username": process.env.NEXT_PUBLIC_GRANTCODE_USERNAME,
        "password": process.env.NEXT_PUBLIC_GRANTCODE_PASSWORD
      });

      const codeHeader = new Headers()
      codeHeader.append("Content-Type", "application/json")
      codeHeader.append("Authorization", `Bearer ${res.access_token}`)
      
      const requestOptions = {
        method: 'POST',
        body: grantCodeRaw,
        headers: codeHeader,
        redirect: 'follow'
      };

      const response = await fetch(`${process.env.NEXT_PUBLIC_GRANTCODE_API_URL}api/pub/std/utils/grantCode`, requestOptions)
      const data = await response.json()
      dispatch(setGrantCode(data.data))

    }

    getGrantCode()

  }, [res])
  return (
    <>
      <Head>
        <title>India International Insurance, Singapore</title>
        <meta name="description" content="Generated by create next app" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <main className="min-h-screen body-bg">
        <Topbar />
        <div className="max-w-5xl mx-auto">
          <div className="grid grid-cols-3 pb-5">
            <div className="flex flex-col bg-[#5BC4BF] p-5 justify-center items-center space-y-4 rounded-tl-2xl rounded-bl-2xl">
              <div className="bg-white rounded-full w-12 h-12 flex items-center justify-center">
                <svg
                  width="30"
                  height="27"
                  viewBox="0 0 71 64"
                  fill="none"
                  xmlns="http://www.w3.org/2000/svg"
                >
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M26.0282 46.9941C26.8183 44.0971 29.4618 36.4886 32.2282 28.364C31.1715 28.7625 30.2077 28.9824 29.013 28.9824C26.0339 28.9824 24.0287 27.622 22.2529 25.4814L18.5327 35.6877C17.7106 37.9574 16.7913 40.5116 15.7679 43.3697C11.9964 53.9084 16.4178 59.8976 22.2395 63.2905C24.6227 63.189 26.049 62.7762 28.1693 62.1578C24.9258 57.0856 24.9341 51.0045 26.0282 46.9941Z"
                    fill="#213F95"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M42.3645 56.0626C41.872 52.7983 42.1666 49.6508 42.8327 47.2116C43.6889 44.0718 49.2537 28.1488 52.2503 19.3468C51.1066 19.7798 50.0621 20.017 48.767 20.017C45.5401 20.017 43.3679 18.5433 41.4446 16.2246L34.7127 34.9631C33.8231 37.423 32.8271 40.1883 31.7182 43.2851C29.0569 50.7206 30.1622 56.0658 32.9984 59.9043L42.3645 56.0626Z"
                    fill="#0065B0"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M35.6024 1.3435C46.5991 -0.340099 55.8864 4.43655 58.766 14.599C62.7563 28.6738 52.9446 47.7461 36.8498 57.1988C22.5711 65.5854 8.14502 63.5806 2.28596 53.1927H0.689453C6.41422 62.2571 19.2051 66.2724 34.0674 62.3966C54.2696 57.1279 70.6473 39.2733 70.6473 22.5171C70.6473 6.18646 55.0923 -3.07048 35.6024 1.3435Z"
                    fill="#57C5BA"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M15.8567 28.8267C15.8567 31.4732 13.735 33.6186 11.1171 33.6186C8.49868 33.6186 6.37695 31.4732 6.37695 28.8267C6.37695 26.1796 8.49868 24.0342 11.1171 24.0342C13.735 24.0342 15.8567 26.1796 15.8567 28.8267Z"
                    fill="#2E307B"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M34.2735 21.7285C34.2735 24.6979 31.8932 27.104 28.9574 27.104C26.0213 27.104 23.6406 24.6979 23.6406 21.7285C23.6406 18.7597 26.0213 16.3525 28.9574 16.3525C31.8932 16.3525 34.2735 18.7597 34.2735 21.7285Z"
                    fill="#213F95"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M54.1173 12.2751C54.1173 15.3777 51.6294 17.8936 48.5607 17.8936C45.4918 17.8936 43.0039 15.3777 43.0039 12.2751C43.0039 9.17283 45.4918 6.65723 48.5607 6.65723C51.6294 6.65723 54.1173 9.17283 54.1173 12.2751Z"
                    fill="#EB1D1F"
                  ></path>
                  <path
                    fillRule="evenodd"
                    clipRule="evenodd"
                    d="M12.3149 59.0653C9.5875 54.8399 9.59092 49.7594 10.5031 46.4138C11.0996 44.2303 12.0466 41.1577 14.0728 35.0731C13.2096 35.402 12.2741 35.5834 11.2969 35.5834C8.79966 35.5834 6.57385 34.4077 5.13161 32.5742L3.57874 37.0188C2.89582 38.9059 2.13165 41.0274 1.28107 43.4023C-4.07643 58.3743 10.4652 62.2962 16.6995 63.2763C14.576 61.9978 13.1957 60.4292 12.3149 59.0653Z"
                    fill="#2E307B"
                  ></path>
                </svg>
              </div>
              <p className="text-[#D7FFFB] text-2xl font-bold leading-6 uppercase">
                Travel Insurance
              </p>
              <div className="flex justify-center items-start flex-col space-y-1">
                <p className="flex space-x-2 items-center text-sm font-medium leading-5 text-white capitalize">
                  <Image src="/plane.png" width={35} height={35} alt="icon" />
                  <span>Affordable Travel Insurance Plans</span>
                </p>
                <p className="flex space-x-2 items-center text-sm font-medium leading-5 text-white capitalize">
                  <Image src="/plane.png" width={35} height={35} alt="icon" />
                  <span>Wide options of coverage</span>
                </p>
                <p className="flex space-x-2 items-center text-sm font-medium leading-5 text-white capitalize">
                  <Image src="/plane.png" width={35} height={35} alt="icon" />
                  <span>Protects you from financial loss</span>
                </p>
                <p className="flex space-x-2 items-center text-sm font-medium leading-5 text-white capitalize">
                  <Image src="/plane.png" width={35} height={35} alt="icon" />
                  <span>most preferred travel insurance partner</span>
                </p>
              </div>
              <div className="relative">
                <Image
                  src="/holiday.png"
                  alt="icon"
                  className="!relative"
                  fill
                  style={{ objectFit: "contain" }}
                />
              </div>
            </div>
            <div className="col-span-2 bg-white/80 rounded-tr-2xl rounded-br-2xl">
              <Accordion />
            </div>
          </div>

          <Footer />
        </div>
      </main>
    </>
  );
}

export async function getServerSideProps() {
  const raw = JSON.stringify({
    "username": process.env.GATEWAY_USERNAME,
    "password": process.env.GATEWAY_PASSWORD
  });
  
  const requestOptions = {
    method: 'POST',
    body: raw,
    redirect: 'follow'
  };

  const response = await fetch(`${process.env.GATEWAY_API_URL}cas/ebao/v1/json/tickets`, requestOptions)
  const data = await response.json()

  return { props: { res: data } }
  

}

