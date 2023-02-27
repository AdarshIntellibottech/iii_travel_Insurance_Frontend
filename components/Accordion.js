import React, { useEffect, useState } from "react";
import AccordionLayout from "./AccordionLayout";
import useFetch from "@/hooks/useFetch";
import { CheckCircleIcon, CalendarIcon } from "@heroicons/react/24/solid";
//import ReactFlagsSelect from "react-flags-select";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import NoTravellers from "./NoTravellers";

function Accordion() {
  const [activeIndex, setActiveIndex] = useState(1);
  const [masterCount, setMasterCount] = useState([]);
  const [policyType, setPolicyType] = useState(null);
  const [policyTypeText, setPolicyTypeText] = useState(null);
  const [startDate, setStartDate] = useState(new Date());
  const [endDate, setEndDate] = useState(new Date());
  const { data: policyTypeData, loading: policyTypeLoading } = useFetch(
    "api/pub/std/utils/codetable",
    "policyType"
  );
  /*   const { data: countryData, loading: countryLoading } = useFetch(
    "api/pub/std/utils/codetable",
    "country"
  ); */
  const { data: travelToData, loading: travelToLoading } = useFetch(
    "api/pub/std/utils/codetable",
    "travelTo"
  );
  const { data: travelPlanData, loading: travelPlanLoading } = useFetch(
    "api/pub/std/utils/codetable",
    "travelPlan"
  );
  const { data: destinationData, loading: destinationLoading } = useFetch(
    "api/pub/std/utils/codetable",
    "destination"
  );
  const [countryCodes, setCountryCodes] = useState([]);
  const [countrySelect, setCountrySelect] = useState("");
  const [countryText, setCountryText] = useState(null);
  const [datePlaceholder, setDatePlaceholder] = useState(null);
  const [travelPlan, setTravelPlan] = useState(null);
  const [travelPlanText, setTravelPlanText] = useState(null);
  const [travellersCount, setTravellersCount] = useState([
    { id: 1, label: "Adults", value: 0 },
    { id: 2, label: "Students", value: 0 },
    { id: 3, label: "Seniors", value: 0 },
    { id: 4, label: "Childrens", value: 0 },
  ]);

  const selectCountry = (e) => {
    setCountrySelect(e.target.value);
    const findText = travelToData?.data?.find(
      (element) => element.id == e.target.value
    );
    const destinationText = destinationData?.data?.find(
      (element) => element.id == e.target.value
    );
    policyType == 1
      ? setCountryText(findText.text)
      : setCountryText(destinationText.text);

    setActiveIndex(3);
  };

  const setCalendarDate = (val) => {
    setEndDate(val);
    const firstDate = `${startDate.getDate()} ${startDate.toLocaleString(
      "default",
      { month: "long" }
    )}, ${startDate.getFullYear()}`;
    const secondDate = `${val.getDate()} ${val.toLocaleString("default", {
      month: "long",
    })}, ${val.getFullYear()}`;
    setDatePlaceholder(`${firstDate} to ${secondDate}`);
    setActiveIndex(4);
  };

  useEffect(() => {
    if (policyType == 2) {
      const date = new Date(startDate);
      const nextYear = date.setFullYear(date.getFullYear() + 1);
      setEndDate(nextYear);
    }
  }, [policyType]);

  /*   useEffect(() => {
    const getCode = () => {
       const countryCode = countryData?.data?.map((item) => item.id);
       console.log(countryCode)
      setCountryCodes(countryCode); 
    };

    getCode();
  }, [countryData, travelToData]); */

  useEffect(() => {
    const updatedVal = travellersCount.map((item) => {
      if (travelPlan !== null) {
        item.value = 0;
      }
      return item;
    });

    setTravellersCount(updatedVal);
  }, [travelPlan]);

  const changePolicyType = (e) => {
    setPolicyType(e.target.value);
    const findText = policyTypeData.data.find(
      (element) => element.id == e.target.value
    );
    setPolicyTypeText(findText.text);
    setActiveIndex(2);
  };

  const changeTravelPlan = (e) => {
    setTravelPlan(e.target.value);
    const findText = travelPlanData.data.find(
      (element) => element.id == e.target.value
    );
    setTravelPlanText(findText.text);
    setActiveIndex(4);
  };

  const chooseTravellers = (message, index) => {
    const updatedCount = { id: index, val: message };
    if (masterCount.length > 0) {
      const exists = masterCount.some((x) => x.id === index);
      if (exists) {
        const updatedData = masterCount.map((x) =>
          x.id === index ? { ...x, val: message } : x
        );
        setMasterCount(updatedData);
      } else {
        setMasterCount((count) => [...count, updatedCount]);
      }
    } else {
      setMasterCount([updatedCount]);
    }
  };

  const increment = (val, id, label) => {
    const updatedVal = travellersCount.map((item) => {
      if (travelPlan == 1) {
        if (item.id == id) {
          if (val >= 1) {
            item.value = item.value;
          } else {
            item.value = item.value + 1;
          }
        } else {
          item.value = 0;
        }
      } else {
        if (item.id == id) {
          if (val >= 2) {
            item.value = item.value;
          } else {
            item.value = item.value + 1;
          }
        }
      }
      return item;
    });
    setTravellersCount(updatedVal);
    chooseTravellers(label, id);
  };

  const decrement = (val, id, label) => {
    const updatedVal = travellersCount.map((item) => {
      if (travelPlan == 1) {
        if (item.id == id) {
          if (val <= 1) {
            item.value = item.value;
          } else {
            item.value = item.value - 1;
          }
        } else {
          item.value = 0;
        }
      } else {
        if (item.id == id) {
          if (val <= 0) {
            item.value = item.value;
          } else {
            item.value = item.value - 1;
          }
        }
      }
      return item;
    });
    setTravellersCount(updatedVal);
    chooseTravellers(label, id);
  };

  useEffect(() => {
    console.log(masterCount);
  }, [masterCount]);

  return (
    <div className="p-5 flex flex-1 flex-col min-h-full">
      <AccordionLayout
        title="What kind of trip you are planning for?"
        index={1}
        activeIndex={activeIndex}
        setActiveIndex={setActiveIndex}
        img="/TripType.png"
        sideBarVal={policyTypeText}
      >
        <div className="flex space-x-4">
          {policyTypeData?.data?.map((item) => (
            <div className="relative flex items-center" key={item.id}>
              <input
                className="sr-only peer policyType"
                type="radio"
                onChange={changePolicyType}
                value={item.id}
                name="policyType"
                checked={policyType == item.id}
                id={`radio${item.id}`}
              />

              <label
                className="flex p-2 space-x-2 cursor-pointer focus:outline-none"
                htmlFor={`radio${item.id}`}
              >
                <CheckCircleIcon
                  className={`policyIcon w-6 h-6 text-[#E0E0E0]`}
                  htmlFor={`radio${item.id}`}
                />
                <span className="text-sm font-semibold">{item.text}</span>
              </label>
            </div>
          ))}
        </div>
      </AccordionLayout>
      <AccordionLayout
        title="Where are you traveling?"
        index={2}
        activeIndex={activeIndex}
        setActiveIndex={setActiveIndex}
        img="/TravelPlace.png"
        sideBarVal={countryText}
      >
        {/* <ReactFlagsSelect
          countries={countryCodes}
          searchable
          selectButtonClassName="!border-0 !border-b"
          onSelect={(code) => selectCountry(code)}
          selected={countrySelect}
        /> */}

        <select
          className=" w-1/2 focus:outline-none border-b 
        border-[#999999] p-2"
          onChange={selectCountry}
        >
          <option value="">
            {policyType == 1 ? "Select Country" : "Select Destination"}
          </option>
          {policyType == 1
            ? travelToData?.data?.map((item) => (
                <option value={item.id} key={item.id}>
                  {item.text}
                </option>
              ))
            : destinationData?.data?.map((item) => (
                <option value={item.id} key={item.id}>
                  {item.text}
                </option>
              ))}
        </select>
      </AccordionLayout>
      <AccordionLayout
        title="What are your travel dates?"
        index={3}
        activeIndex={activeIndex}
        setActiveIndex={setActiveIndex}
        img="/TravelDate.png"
        sideBarVal={datePlaceholder}
      >
        <div className="flex justify-between space-x-8">
          <div className="flex space-x-2">
            <p className="text-[#565656] font-semibold text-sm">Leaving on</p>
            <div className="flex space-x-1 border-b border-[#999999]">
              <DatePicker
                showIcon
                selected={startDate}
                onChange={(date) => setStartDate(date)}
                selectsStart
                minDate={new Date()}
                startDate={startDate}
                endDate={endDate}
                dateFormat="MMMM d, yyyy"
                className="focus:outline-none font-sans text-sm font-semibold"
              />
              <CalendarIcon className="w-6 h-6 text-[#21409A]" />
            </div>
          </div>

          <div className="flex space-x-1">
            <p className="text-[#565656] font-semibold text-sm">Returning on</p>
            <div className="flex space-x-1 border-b border-[#999999]">
              <DatePicker
                selected={endDate}
                showIcon
                onChange={(date) => setCalendarDate(date)}
                selectsEnd
                startDate={startDate}
                endDate={endDate}
                minDate={startDate}
                disabled={policyType == 2}
                dateFormat="MMMM d, yyyy"
                maxDate={
                  new Date(startDate.getTime() + 180 * 24 * 60 * 60 * 1000)
                }
                className="focus:outline-none font-sans text-sm font-semibold"
              />
              <CalendarIcon className="w-6 h-6 text-[#21409A]" />
            </div>
          </div>
        </div>
      </AccordionLayout>
      <AccordionLayout
        title="Who all are traveling?"
        index={4}
        activeIndex={activeIndex}
        setActiveIndex={setActiveIndex}
        img="/Travelers.png"
        sideBarVal={travelPlanText}
      >
        <div className="flex flex-col">
          {travelPlanData?.data?.map((item) => (
            <div className="relative flex items-center" key={item.id}>
              <input
                className="sr-only peer policyType"
                type="radio"
                onChange={changeTravelPlan}
                value={item.id}
                name="travelPlan"
                checked={travelPlan == item.id}
                id={`travelPlan${item.id}`}
              />

              <label
                className="flex p-2 space-x-2 cursor-pointer focus:outline-none"
                htmlFor={`travelPlan${item.id}`}
              >
                <CheckCircleIcon
                  className={`policyIcon w-6 h-6 text-[#E0E0E0]`}
                  htmlFor={`travelPlan${item.id}`}
                />
                <span className="text-sm font-semibold">{item.text}</span>
              </label>
            </div>
          ))}
          {travelPlan !== null ? (
            <div className="grid grid-cols-2 justify-center">
              {travellersCount?.map((item) => (
                <NoTravellers
                  key={item.id}
                  label={item.label}
                  countVal={item.value}
                  incrementCount={() =>
                    increment(item.value, item.id, item.label)
                  }
                  decrementCount={() =>
                    decrement(item.value, item.id, item.label)
                  }
                />
              ))}
            </div>
          ) : null}
        </div>
      </AccordionLayout>

      <div className="mt-auto">
        <div className="flex space-x-2 w-full">
          <div className="flex flex-1 rounded-full border border-[#E0E0E0] bg-white px-4 py-2">
            <input
              type="text"
              className="w-full outline-none"
              placeholder="Enter promocode if you have any"
            />
            <button className="font-bold text-[#21409A] text-sm">Apply</button>
          </div>
          <button
            className="bg-[#21409A] disabled:bg-[#21409A33] text-white px-8 py-2 rounded-full 
          uppercase"
            disabled={masterCount.length == 0}
          >
            Get Quote
          </button>
        </div>
      </div>
    </div>
  );
}

export default Accordion;
