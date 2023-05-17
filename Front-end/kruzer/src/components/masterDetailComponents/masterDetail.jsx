import { Badge, Dropdown, Space, Table } from "antd";
import axios from "axios";
import { useState, useEffect } from "react";

const MasterDetail = () => {
    return (
        <Table
            columns={columns}
            expandable={{
            expandedRowRender,
            defaultExpandedRowKeys: ["0"],
            }}
            dataSource={data}
        />
       
    );
}

export default MasterDetail;
