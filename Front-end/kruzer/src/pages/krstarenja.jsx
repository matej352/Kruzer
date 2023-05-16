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
  /*fetch(
    "https://localhost:7295/api/Krstarenje/GetAll",
  ).then((response) => { return response.json()})
   .then((response) => { console.log("response ", response)});*/
  useEffect(() => {
    
    api.get('/api/Krstarenje/GetAll').then((response) => {
      console.log(response.data);
    }).catch((error) => {
      console.error(error);
    });

    
    
  }, [])
  
  const expandedRowRender = () => {
    const columns = [
      {
        title: "ID",
        dataIndex: "passengerID",
        key: "reservationID",
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
            <a>Obriši</a>
            <a>Uredi</a>
          </Space>
        ),
      },
    ];
    const data = [];
    for (let i = 0; i < 3; ++i) {
      data.push({
        key: i.toString(),
        date: "2014-12-24 23:12:00",
        name: "This is production name",
        upgradeNum: "Upgraded: 56",
      });
    }
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
