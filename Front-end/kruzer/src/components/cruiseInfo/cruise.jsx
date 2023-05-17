import { Card, Typography, Button, Space } from "antd";
const { Title } = Typography;

function Cruise({ cruise = {}, setCurrentCruise, numberOfCruises }) {
  const {
    datumkraj,
    datumpocetak,
    id,
    kapacitet,
    lokacije,
    naslov,
    opis,
    popunjenost,
  } = cruise;

  function handlePreviousCruise() {
    if (id == 1) return;
    else {
      setCurrentCruise((current) => current - 1);
    }
  }

  function handleNextCruise() {
    if (id == numberOfCruises) return;
    else {
      setCurrentCruise((current) => current + 1);
    }
  }

  return (
    <Card
      style={{
        width: "100%",
        margin: "20px",
      }}
    >
      <Title underline={true} level={2}>
        Krstarenje
      </Title>
      <Title level={4}>{naslov}</Title>
      <Title level={5}>{opis}</Title>

      <p>
        <strong>Trajanje putovanja:</strong> {datumpocetak} <strong>-</strong>{" "}
        {datumkraj}
      </p>
      <p>
        <strong>Destinacije: </strong>
        {lokacije?.map((lokacija, i) =>
          i == lokacije.length - 1 ? (
            <span>
              {lokacija.grad} ({lokacija.država}){""}
            </span>
          ) : (
            <span>
              {lokacija.grad} ({lokacija.država}){",  "}
            </span>
          )
        )}
      </p>
      <p>
        <strong>Maksimalni kapacitet:</strong> {kapacitet}
      </p>
      <p>
        <strong>Trenutna popunjenost:</strong> {popunjenost}
      </p>
      <div className="flex justify-end">
        <Space>
          <Button onClick={handlePreviousCruise}>Prethodno</Button>
          <Button onClick={handleNextCruise}>Iduće</Button>
        </Space>
      </div>
    </Card>
  );
}

export default Cruise;
