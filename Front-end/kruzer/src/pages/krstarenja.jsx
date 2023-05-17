import { DownOutlined } from "@ant-design/icons";
import { Badge, Dropdown, Space, Table } from "antd";
import axios from "axios";
import { useState, useEffect } from "react";

const api = axios.create({
  baseURL: 'https://localhost:7295', 
  withCredentials: false,
});

const items = [
  {
    key: "1",
    label: "Action 1",
  },
  {
    key: "2",
    label: "Action 2",
  },
];
const Krstarenja = () => {
  const [krstarenja, setKrstarenja] = useState([])

  useEffect(() => {
    
    api.get('/api/Krstarenje/GetAll').then((response) => {
      console.log(response.data);
      setKrstarenja(response.data)
    }).catch((error) => {
      console.error(error);
    });

  }, [])
  
  const expandedRowRender = () => {
    const columns = [
      {
        title: "ID",
        dataIndex: "passengerID",
        key: "passengerID",
      },
      {
        title: "Ime",
        dataIndex: "passengerName",
        key: "passengerName",
      },
      {
        title: "Prezime",
        dataIndex: "passengerSurname",
        key: "passengerSurname",
      },
      {
        title: "Nadimak",
        dataIndex: "passengerNickname",
        key: "passengerNickname",
      },
      {
        title: "Email",
        dataIndex: "passengerEmail",
        key: "passengerEmail",
      },
      {
        title: "Spol",
        dataIndex: "passengerGender",
        key: "passengerGender",
      },
      {
        title: "",
        dataIndex: "operation",
        key: "operation",
        render: () => (
          <Space size="middle">
            <a onClick={() => {console.log(data)}}>Obriši</a>
            <a>Uredi</a>
          </Space>
        ),
      },
    ];

    const data = [];
    let putnik = krstarenja[0]?.rezervacije[0]?.putnik
    data.push({
      key: putnik?.id,
      passengerID: putnik?.id,
      passengerName: putnik?.ime,
      passengerSurname: putnik?.prezime,
      passengerNickname:putnik?.nadimak,
      passengerEmail:putnik?.email,
      passengerGender:putnik?.spol,
      dataIndex: putnik?.id
      //data index mozda pomogne za otvaranje modala da se zna za kojeg je
    });
    console.log("data ", data, krstarenja)
    
    return <Table columns={columns} dataSource={data} pagination={false} />;
  };


  const columns = [
    {
      title: "ID",
      dataIndex: "reservationID",
      key: "reservationID",
    },
    {
      title: "Vrijeme",
      dataIndex: "dateAndTime",
      key: "dateAndTime",
    },
    {
      title: "Broj putnika",
      dataIndex: "passengerCount",
      key: "passengerCount",
    },
    {
      title: "",
      key: "operation",
      render: () => <a onClick={() => setModalVisible(true)}>Uredi</a>,
    },
  ];


  const data = [];
  for (let i = 0; i < 3; ++i) {
    data.push({
      key: i.toString(),
      name: "Screen",
      platform: "iOS",
      version: "10.3.4.5654",
      upgradeNum: 500,
      creator: "Jack",
      createdAt: "2014-12-24 23:12:00",
    });
  }


  return (
    <>
      <Table
        columns={columns}
        expandable={{
          expandedRowRender,
          defaultExpandedRowKeys: ["0"],
        }}
        dataSource={data}
      />
    </>
  );
};
export default Krstarenja;
