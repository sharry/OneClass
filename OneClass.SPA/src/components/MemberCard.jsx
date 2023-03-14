function MemberCard({id, name, image}){
    return <>
    <div className="flex items-center py-1">
        <img
          alt=""
          src="https://source.unsplash.com/100x100/?portrait"
          className="ml-3 object-cover w-14 h-14 rounded-full shadow dark:bg-gray-200"
        />
        <p className="ml-4 font-medium">{name}</p>
      </div></>
}

export default MemberCard;