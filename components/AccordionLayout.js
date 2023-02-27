import Image from "next/image";
import React from "react";

function AccordionLayout({
  title,
  children,
  index,
  activeIndex,
  setActiveIndex,
  img,
  sideBarVal,
}) {
  const handleSetIndex = (index) => {
    if (sideBarVal !== null) activeIndex !== index && setActiveIndex(index);
  };

  return (
    <>
      <div
        className={`${
          activeIndex === index
            ? "bg-white shadow-[0_0px_8px_0px_rgba(4,117,236,0.2)] box-border"
            : "bg-[#E7F2FC]"
        } flex flex-col justify-between p-5 mb-4  rounded-xl cursor-pointer`}
      >
        <div
          onClick={() => handleSetIndex(index)}
          className="flex items-center"
        >
          <div className="flex items-center space-x-2">
            <Image src={img} width={32} height={32} alt="icon" />
            <div className="text-[#565656] font-normal leading-4">{title}</div>
          </div>
          {activeIndex !== index && (
            <p className="ml-auto text-sm font-semibold">{sideBarVal}</p>
          )}
        </div>
        {activeIndex === index && (
          <div className="shadow-3xl rounded-2xl shadow-cyan-500/50 px-10 pt-4 pb-1">
            {children}
          </div>
        )}
      </div>
    </>
  );
}

export default AccordionLayout;
