import { ChevronRightIcon } from '@heroicons/react/24/solid'

function Topbar() {
  return (
    <div className='max-w-full px-5 py-2'>
      <div className='flex'>
        <div className='flex space-x-2 justify-start items-center'>
            <p className='text-[#21409A] text-sm'>Home </p>
            <ChevronRightIcon className='h-3 text-[#21409A]' />
            <p className='text-[#21409A] text-sm'>Our Products </p>
            <ChevronRightIcon className='h-3 text-[#21409A]' />
            <p className='text-[#21409A] text-sm'>Travel Insurance Home </p>
            <ChevronRightIcon className='h-3 text-[#21409A]' />
            <p className='text-[#565656] text-sm'>Buy Travel Insurance</p>
        </div>
        <button className='ml-auto text-[#21409A] text-sm font-bold uppercase border 
        border-[#21409A] py-1 px-5 rounded-full cursor-pointer'>faqs</button>
        </div>
        </div>
  )
}

export default Topbar