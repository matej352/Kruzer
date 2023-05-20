import { Modal, Form, Input, notification, Table, Button } from "antd";
import { useState, useEffect } from "react";
import { api } from "@/src/core/api";

function LocationModal({ visible, setVisible }) {
  const [lokacije, setLokacije] = useState([]);
  const [refetch, setRefetch] = useState(false);

  const columns = [
    {
      title: "Grad",
      dataIndex: "grad",
      key: "grad",
    },
    {
      title: "Država",
      dataIndex: "država",
      key: "država",
    },
    {
      title: "",
      key: "operation",
      render: (record) => (
        <a
          onClick={() => {
            handleLocationDelete(record.id);
          }}
        >
          Obriši
        </a>
      ),
    },
  ];

  useEffect(() => {
    api
      .get("/api/Lokacija/GetAll")
      .then((response) => {
        console.log(response.data);
        setLokacije(response.data);
      })
      .catch((error) => {
        console.error(error);
      });
  }, [refetch]);

  const handleCancel = () => {
    setVisible(false);
  };

  async function handleLocationDelete(locationId) {
    const response = await api.delete("/api/Lokacija/" + locationId);
    console.log("response ", response);
    if (response.status == 204 || response.status == 200) {
      notification.open({
        message: "Lokacija obrisana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  async function onFinish() {
    const data = form.getFieldsValue();
    const response = await api.post("/api/Lokacija/Create", data);

    if (response.status == 204 || response.status == 200) {
      notification.open({
        message: "Lokacija dodana!",
      });
      setRefetch((prev) => !prev);
    } else {
      notification.open({
        message: "Dogodila se pogreška, pokušajte ponovno!",
      });
    }
  }

  const [form] = Form.useForm();

  return (
    <>
      <Modal
        title="Lokacije"
        open={visible}
        onCancel={handleCancel}
        cancelText="Natrag"
        okButtonProps={{ style: { display: "none" } }}
      >
        <Form
          form={form}
          layout="vertical"
          autoComplete="off"
          name="validateOnly"
          onFinish={onFinish}
          style={{
            margin: "20px",
            width: "70%",
          }}
        >
          <Form.Item
            name="grad"
            label="Grad"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="država"
            label="Država"
            rules={[
              {
                required: true,
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Button type="primary" htmlType="submit">
            Dodaj
          </Button>
        </Form>
        <Table
          style={{
            margin: "20px",
          }}
          columns={columns}
          expandable={{
            rowExpandable: (record) => record?.name === "Not Expandable",
          }}
          dataSource={lokacije}
        />
      </Modal>
    </>
  );
}
export default LocationModal;