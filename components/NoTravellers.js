import { MinusCircleIcon, PlusCircleIcon } from "@heroicons/react/24/outline";

function NoTravellers({ label, countVal, incrementCount, decrementCount }) {


  return (
    <div className="flex justify-around mb-8">
      <div>
        <span className="font-semibold text-sm">{label}</span>
      </div>
      <div className="flex space-x-4 justify-center">
        <MinusCircleIcon
          className="w-6 h-6 text-[#33C5B4]"
          onClick={decrementCount}
        />
        <span className="text-base text-[#565656] font-semibold">{countVal}</span>
        <PlusCircleIcon
          className="w-6 h-6 text-[#33C5B4]"
          onClick={incrementCount}
        />
      </div>
    </div>
  );
}

export default NoTravellers;
