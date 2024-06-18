const CurrentDate = () => {
  const date = new Date();

  const day = date.getDate();
  const month = date.toLocaleString('ru-RU', { month: 'long' });
  const year = date.getFullYear();

  const formattedDate = `${day} число, ${month.charAt(0).toUpperCase() + month.slice(1)}, ${year} года`;

  return (formattedDate);
}

export default CurrentDate;
